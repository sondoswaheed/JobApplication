using JobApplication.Application.DTOs.Applications;
using JobApplication.Application.Interfaces;
using JobApplication.Application.Interfaces.Repositories;
using JobApplication.Domain.Entities;

namespace JobApplication.Application.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly ICurrentUserService _currentUserService;

        public ApplicationService(
            IApplicationRepository applicationRepository,
            ICandidateRepository candidateRepository,
            ICurrentUserService currentUserService)
        {
            _applicationRepository = applicationRepository;
            _candidateRepository = candidateRepository;
            _currentUserService = currentUserService;
        }

        public async Task<int> CreateApplicationAsync(
            CreateApplicationDto dto)
        {
            var candidate = await _candidateRepository
                .GetByIdAsync(dto.CandidateId);

            if (candidate == null)
                throw new InvalidOperationException(
                    "Candidate not found.");

            var application = new Applicationn
            {
                CandidateId = dto.CandidateId,
                JobId = dto.JobId,
                Status = "Applied",
                AppliedAt = DateTime.UtcNow
            };

            await _applicationRepository.AddAsync(application);

            return application.Id;
        }

        public async Task<bool> CancelApplicationAsync(int id)
        {
            var application =
                await _applicationRepository.GetByIdAsync(id);

            if (application == null)
                return false;

            var currentUserId = _currentUserService.UserId;

            if (string.IsNullOrEmpty(currentUserId))
                throw new UnauthorizedAccessException();

         

            if (application.Status != "Applied" &&
                application.Status != "UnderReview")
            {
                throw new InvalidOperationException(
                    "Application cannot be cancelled in its current status."
                );
            }

            application.Status = "Cancelled";
            application.CancelledAt = DateTime.UtcNow;
            application.StatusUpdatedAt = DateTime.UtcNow;

            await _applicationRepository.UpdateAsync(application);

            return true;
        }
    }
}
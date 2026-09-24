using JobApplication.Application.Interfaces;
using JobApplication.Application.Interfaces.Repositories;
using JobApplication.Domain.Entities;
using JobApplication.Domain.Enums;
using MediatR;

namespace JobApplication.Application.Features.Command.CreateApplication
{
    public class CreateApplicationCommandHandler : IRequestHandler<CreateApplicationCommand, int>
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly IJobRepository _jobRepository;
        private readonly ICurrentUserService _currentUserService;

        public CreateApplicationCommandHandler( IApplicationRepository applicationRepository, ICandidateRepository candidateRepository,
            IJobRepository jobRepository, ICurrentUserService currentUserService)
        {
            _applicationRepository = applicationRepository;
            _candidateRepository = candidateRepository;
            _jobRepository = jobRepository;
            _currentUserService = currentUserService;
        }

        public async Task<int> Handle( CreateApplicationCommand request, CancellationToken cancellationToken)
        {
            // 1. Get current logged-in user
            var currentUserId = _currentUserService.UserId;

            if (string.IsNullOrEmpty(currentUserId))
                throw new UnauthorizedAccessException(
                    "User is not authenticated."
                );

            var candidate = await _candidateRepository.GetByUserIdAsync(currentUserId);

            if (candidate == null)
                throw new InvalidOperationException(
                    "Candidate profile not found."
                );


            var job =
                await _jobRepository
                    .GetByIdAsync(request.JobId);

            if (job == null)
                throw new KeyNotFoundException(
                    "Job not found."
                );

            if (!job.IsActive)
                throw new InvalidOperationException(
                    "Cannot apply for a closed job."
                );

            var alreadyApplied = await _applicationRepository.ExistsAsync( candidate.Id, request.JobId);

            if (alreadyApplied)
                throw new InvalidOperationException("You have already applied for this job."
                );


            var application = new Applicationn
            {
                CandidateId = candidate.Id,
                JobId = request.JobId,
                Status = ApplicationStatus.Applied,
                AppliedAt = DateTime.UtcNow,
                StatusUpdatedAt = DateTime.UtcNow
            };


            await _applicationRepository.AddAsync(application);

            return application.Id;
        }
    }
}
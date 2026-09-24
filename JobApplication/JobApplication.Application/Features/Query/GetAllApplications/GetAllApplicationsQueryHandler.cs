using JobApplication.Application.DTOs.Applications;
using JobApplication.Application.Interfaces;
using JobApplication.Application.Interfaces.Repositories;
using MediatR;

namespace JobApplication.Application.Features.Query.GetAllApplications
{
    public class GetAllApplicationsQueryHandler: IRequestHandler< GetAllApplicationsQuery,  List<ApplicationResponseDto>>
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IJobRepository _jobRepository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetAllApplicationsQueryHandler(IApplicationRepository applicationRepository, IJobRepository jobRepository,
            ICandidateRepository candidateRepository, ICurrentUserService currentUserService)
        {
            _applicationRepository = applicationRepository;
            _jobRepository = jobRepository;
            _candidateRepository = candidateRepository;
            _currentUserService = currentUserService;
        }

        public async Task<List<ApplicationResponseDto>> Handle( GetAllApplicationsQuery request, CancellationToken cancellationToken)
        {
            var recruiterId = _currentUserService.UserId;

            if (string.IsNullOrEmpty(recruiterId))
                throw new UnauthorizedAccessException( "User is not authenticated.");

            var applications = await _applicationRepository.GetAllAsync();

            var result = new List<ApplicationResponseDto>();

            foreach (var application in applications)
            {
                var job = await _jobRepository.GetByIdAsync(application.JobId);

                if (job == null)
                    continue;

                if (job.RecruiterId != recruiterId)
                    continue;

                var candidate = await _candidateRepository
                    .GetByIdAsync(application.CandidateId);

                if (candidate == null)
                    continue;

                result.Add(new ApplicationResponseDto
                {
                    Id = application.Id,
                    CandidateId = candidate.Id,
                    CandidateName = candidate.Name,
                    JobId = job.Id,
                    JobTitle = job.Title,
                    Status = application.Status,
                    AppliedAt = application.AppliedAt,
                    StatusUpdatedAt = application.StatusUpdatedAt
                });
            }

            return result;
        }
    }
}
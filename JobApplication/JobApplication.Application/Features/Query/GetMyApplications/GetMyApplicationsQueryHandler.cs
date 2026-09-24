using JobApplication.Application.DTOs.Applications;
using JobApplication.Application.Interfaces;
using JobApplication.Application.Interfaces.Repositories;
using MediatR;

namespace JobApplication.Application.Features.Query.GetMyApplications
{
    public class GetMyApplicationsQueryHandler: IRequestHandler<GetMyApplicationsQuery,List<ApplicationResponseDto>>
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IJobRepository _jobRepository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetMyApplicationsQueryHandler(IApplicationRepository applicationRepository, IJobRepository jobRepository,
            ICandidateRepository candidateRepository,ICurrentUserService currentUserService)
        {
            _applicationRepository = applicationRepository;
            _jobRepository = jobRepository;
            _candidateRepository = candidateRepository;
            _currentUserService = currentUserService;
        }

        public async Task<List<ApplicationResponseDto>> Handle( GetMyApplicationsQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException( "User is not authenticated.");

            var candidate =await _candidateRepository.GetByUserIdAsync(userId);

            if (candidate == null)
                throw new InvalidOperationException("Candidate profile not found.");

            var applications = await _applicationRepository.GetByCandidateIdAsync(candidate.Id);

            var result = new List<ApplicationResponseDto>();

            foreach (var application in applications)
            {
                var job = await _jobRepository .GetByIdAsync(application.JobId);

                if (job == null)
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
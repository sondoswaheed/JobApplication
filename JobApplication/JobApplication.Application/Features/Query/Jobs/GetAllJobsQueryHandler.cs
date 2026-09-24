using JobApplication.Application.DTOs.Jobs;
using JobApplication.Application.Features.Query.Jobs;
using JobApplication.Application.Interfaces.Repositories;
using MediatR;

namespace JobApplication.Application.Features.Query.GetAllJobs
{
    public class GetAllJobsQueryHandler : IRequestHandler<GetAllJobsQuery, List<JobResponseDto>>
    {
        private readonly IJobRepository _jobRepository;

        public GetAllJobsQueryHandler(
            IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<List<JobResponseDto>> Handle( GetAllJobsQuery request, CancellationToken cancellationToken)
        {
            var jobs = await _jobRepository.GetAllAsync();

            return jobs.Where(j => j.IsActive)
                .Select(j => new JobResponseDto
                {
                    Id = j.Id,
                    Title = j.Title,
                    IsActive = j.IsActive,
                    ClosedAt = j.ClosedAt
                })
                .ToList();
        }
    }
}
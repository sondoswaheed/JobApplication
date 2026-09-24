using JobApplication.Application.DTOs.Jobs;
using MediatR;

namespace JobApplication.Application.Features.Query.Jobs
{
    public class GetAllJobsQuery : IRequest<List<JobResponseDto>>
    {
    }
}

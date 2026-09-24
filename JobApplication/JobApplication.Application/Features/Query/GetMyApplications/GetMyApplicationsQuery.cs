using JobApplication.Application.DTOs.Applications;
using MediatR;

namespace JobApplication.Application.Features.Query.GetMyApplications
{
    public class GetMyApplicationsQuery : IRequest<List<ApplicationResponseDto>>
    {
    }
}

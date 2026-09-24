using JobApplication.Application.DTOs.Applications;
using MediatR;

namespace JobApplication.Application.Features.Query.GetAllApplications
{
    public class GetAllApplicationsQuery : IRequest<List<ApplicationResponseDto>>
    {
    }
}
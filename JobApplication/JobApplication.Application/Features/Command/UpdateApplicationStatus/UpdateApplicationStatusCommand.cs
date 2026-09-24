using JobApplication.Domain.Enums;
using MediatR;

namespace JobApplication.Application.Features.Command.UpdateApplicationStatus
{
    public class UpdateApplicationStatusCommand : IRequest<bool>
    {
        public int ApplicationId { get; set; }

        public ApplicationStatus Status { get; set; } 
    }
}
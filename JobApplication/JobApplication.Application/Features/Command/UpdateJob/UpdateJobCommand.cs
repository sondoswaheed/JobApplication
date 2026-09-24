using MediatR;

namespace JobApplication.Application.Features.Command.UpdateJob
{
    public class UpdateJobCommand : IRequest<bool>
    {
        public int JobId { get; set; }

        public string Title { get; set; } = string.Empty;
    }
}
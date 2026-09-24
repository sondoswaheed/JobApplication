using MediatR;

namespace JobApplication.Application.Features.Command.UpdateCandidate
{
    public class UpdateCandidateCommand : IRequest<bool>
    {
        public string Name { get; set; } = string.Empty;

        public string CVUrl { get; set; } = string.Empty;
    }
}
using JobApplication.Application.Interfaces;
using JobApplication.Application.Interfaces.Repositories;
using MediatR;

namespace JobApplication.Application.Features.Command.UpdateCandidate
{
    public class UpdateCandidateCommandHandler : IRequestHandler<UpdateCandidateCommand, bool>
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly ICurrentUserService _currentUserService;

        public UpdateCandidateCommandHandler(ICandidateRepository candidateRepository, ICurrentUserService currentUserService)
        {
            _candidateRepository = candidateRepository;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle( UpdateCandidateCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException( "User is not authenticated.");

            var candidate = await _candidateRepository .GetByUserIdAsync(userId);

            if (candidate == null)
                throw new InvalidOperationException( "Candidate profile not found." );

            candidate.Name = request.Name.Trim();
            candidate.CVUrl = request.CVUrl.Trim();

            await _candidateRepository.UpdateAsync(candidate);

            return true;
        }
    }
}
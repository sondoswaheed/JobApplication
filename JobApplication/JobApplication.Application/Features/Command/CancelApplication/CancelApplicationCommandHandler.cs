using JobApplication.Application.Interfaces;
using JobApplication.Application.Interfaces.Repositories;
using JobApplication.Domain.Entities;
using JobApplication.Domain.Enums;
using MediatR;

namespace JobApplication.Application.Features.Command.CancelApplication
{
    public class CancelApplicationCommandHandler : IRequestHandler<CancelApplicationCommand, bool>
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly ICurrentUserService _currentUserService;

        public CancelApplicationCommandHandler(IApplicationRepository applicationRepository, ICandidateRepository candidateRepository, ICurrentUserService currentUserService)
        {
            _applicationRepository = applicationRepository;
            _candidateRepository = candidateRepository;
            _currentUserService = currentUserService;
        }
        public async Task<bool> Handle(CancelApplicationCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException( "User is not authenticated." );

            var candidate = await _candidateRepository.GetByUserIdAsync(userId);

            if (candidate == null)
                throw new InvalidOperationException( "Candidate profile not found." );

            var application =  await _applicationRepository .GetByIdAsync(request.id);

            if (application == null)
                return false;

            if (application.CandidateId != candidate.Id)
                throw new UnauthorizedAccessException(
                    "You cannot cancel another candidate's application."
                );

            if (application.Status != ApplicationStatus.Cancelled &&
                application.Status != ApplicationStatus.UnderReview)
            {
                throw new InvalidOperationException( "Application cannot be cancelled in its current status."
                );
            }

            application.Status = ApplicationStatus.Cancelled;
            application.CancelledAt = DateTime.UtcNow;
            application.StatusUpdatedAt = DateTime.UtcNow;

            await _applicationRepository.UpdateAsync(application);

            return true;
        }
    }
    }


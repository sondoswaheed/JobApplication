using JobApplication.Application.Interfaces;
using JobApplication.Application.Interfaces.Repositories;
using JobApplication.Domain.Entities;
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
            var application = await _applicationRepository.GetByIdAsync(request.id);

            if (application == null)
                return false;

            var currentUserId = _currentUserService.UserId;

            if (string.IsNullOrEmpty(currentUserId))
                throw new UnauthorizedAccessException();



            if (application.Status != "Applied" &&
                application.Status != "UnderReview")
            {
                throw new InvalidOperationException(
                    "Application cannot be cancelled in its current status."
                );
            }

            application.Status = "Cancelled";
            application.CancelledAt = DateTime.UtcNow;
            application.StatusUpdatedAt = DateTime.UtcNow;

            await _applicationRepository.UpdateAsync(application);

            return true;
        }
    }
    }


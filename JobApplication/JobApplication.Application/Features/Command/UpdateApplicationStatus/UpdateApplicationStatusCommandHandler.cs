using JobApplication.Application.Interfaces;
using JobApplication.Application.Interfaces.Repositories;
using JobApplication.Domain.Enums;
using MediatR;

namespace JobApplication.Application.Features.Command.UpdateApplicationStatus
{
    public class UpdateApplicationStatusCommandHandler: IRequestHandler<UpdateApplicationStatusCommand, bool>
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IJobRepository _jobRepository;
        private readonly ICurrentUserService _currentUserService;

        public UpdateApplicationStatusCommandHandler( IApplicationRepository applicationRepository, IJobRepository jobRepository, ICurrentUserService currentUserService)
        {
            _applicationRepository = applicationRepository;
            _jobRepository = jobRepository;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle( UpdateApplicationStatusCommand request, CancellationToken cancellationToken)
        {
            var recruiterId = _currentUserService.UserId;

            if (string.IsNullOrEmpty(recruiterId))
                throw new UnauthorizedAccessException( "User is not authenticated." );

            var application = await _applicationRepository.GetByIdAsync(request.ApplicationId);

            if (application == null)
                return false;

            var job =  await _jobRepository.GetByIdAsync(application.JobId);

            if (job == null)
                throw new KeyNotFoundException( "Related job not found." );

            if (job.RecruiterId != recruiterId)
                throw new UnauthorizedAccessException( "You cannot update applications for another recruiter's job.");

            if (application.Status == ApplicationStatus.Cancelled)
                throw new InvalidOperationException("Cancelled applications cannot be updated." );

            if (application.Status == ApplicationStatus.Accepted ||  application.Status == ApplicationStatus.Rejected)
            {
                throw new InvalidOperationException("Finalized applications cannot be updated.");
            }

            application.Status = request.Status;
            application.StatusUpdatedAt = DateTime.UtcNow;

            await _applicationRepository.UpdateAsync(application);

            return true;
        }
    }
}
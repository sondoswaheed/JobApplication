using JobApplication.Application.Interfaces;
using JobApplication.Application.Interfaces.Repositories;
using MediatR;

namespace JobApplication.Application.Features.Command.UpdateJob
{
    public class UpdateJobCommandHandler: IRequestHandler<UpdateJobCommand, bool>
    {
        private readonly IJobRepository _jobRepository;
        private readonly ICurrentUserService _currentUserService;

        public UpdateJobCommandHandler( IJobRepository jobRepository,ICurrentUserService currentUserService)
        {
            _jobRepository = jobRepository;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle( UpdateJobCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException(
                    "User is not authenticated."
                );

            var job = await _jobRepository.GetByIdAsync(request.JobId);

            if (job == null)
                return false;

            if (job.RecruiterId != userId)
                throw new UnauthorizedAccessException(
                    "You cannot update another recruiter's job."
                );

            if (!job.IsActive)
                throw new InvalidOperationException(
                    "Closed jobs cannot be updated."
                );

            job.Title = request.Title.Trim();

            await _jobRepository.UpdateAsync(job);

            return true;
        }
    }
}
using JobApplication.Application.Interfaces;
using JobApplication.Application.Interfaces.Repositories;
using JobApplication.Domain.Entities;


namespace JobApplication.Application.Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;
        private readonly ICurrentUserService _currentUserService;

        public JobService(IJobRepository jobRepository , ICurrentUserService currentUser)
        {
            _jobRepository = jobRepository;
            _currentUserService = currentUser; 
        }
        public async Task<bool> CloseJobAsync(int jobId)
        {
            var job = await _jobRepository.GetByIdAsync(jobId);

            if (job == null)
                return false;

            var CurrentUSerID = _currentUserService.UserId;

            if (CurrentUSerID != job.RecruiterId)
                throw new UnauthorizedAccessException();

            job.Close(CurrentUSerID);

            await _jobRepository.UpdateAsync(job);

            return true;
        }

        public async Task<int> CreateJobAsync(string title)
        {
            var currentUserId = _currentUserService.UserId;

            if (string.IsNullOrEmpty(currentUserId))
                throw new UnauthorizedAccessException();

            var job = new Job
            {
                Title = title,
                IsActive = true,
                RecruiterId = currentUserId,
                ClosedAt = null,
                ClosedBy = null
            };

            await _jobRepository.AddAsync(job);

            return job.Id;
        }
    }
}

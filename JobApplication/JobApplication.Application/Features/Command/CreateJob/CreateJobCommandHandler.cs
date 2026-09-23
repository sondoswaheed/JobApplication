using JobApplication.Application.Interfaces;
using JobApplication.Application.Interfaces.Repositories;
using JobApplication.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Application.Features.Command.CreateJob
{
    public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, int>
    {
        private readonly IJobRepository _jobRepository;
        private readonly ICurrentUserService _currentUserService;

        public CreateJobCommandHandler(IJobRepository jobRepository, ICurrentUserService currentUser)
        {
            _jobRepository = jobRepository;
            _currentUserService = currentUser;
        }
        public async Task<int> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.UserId;

            if (string.IsNullOrEmpty(currentUserId))
                throw new UnauthorizedAccessException();

            var job = new Job
            {
                Title = request.Title,
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

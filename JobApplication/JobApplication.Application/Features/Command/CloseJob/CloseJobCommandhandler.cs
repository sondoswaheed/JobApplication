using JobApplication.Application.Interfaces;
using JobApplication.Application.Interfaces.Repositories;
using JobApplication.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Application.Features.Command.CloseJob
{
    public class CloseJobCommandhandler : IRequestHandler<CloseJobCommand,bool>
    {
        private readonly IJobRepository _jobRepository;
        private readonly ICurrentUserService _currentUserService;
        public CloseJobCommandhandler(IJobRepository jobRepository, ICurrentUserService currentUser)
        {
            _jobRepository = jobRepository;
            _currentUserService = currentUser;
        }

        public async Task<bool> Handle(CloseJobCommand request, CancellationToken cancellationToken)
        {

            var userId = _currentUserService.UserId;

            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException( "User is not authenticated." );

            var job =  await _jobRepository.GetByIdAsync(request.JobId);

            if (job == null)
                return false;

            if (job.RecruiterId != userId)
                throw new UnauthorizedAccessException( "You cannot close another recruiter's job." );

            if (!job.IsActive)
                throw new InvalidOperationException( "Job is already closed." );

            job.Close(userId);

            await _jobRepository.UpdateAsync(job);

            return true;
        }

     
    }
}

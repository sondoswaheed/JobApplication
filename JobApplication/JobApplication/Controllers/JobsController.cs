using JobApplication.Application.DTOs.Jobs;
using JobApplication.Application.Features.Command.CreateJob;
using JobApplication.Application.Features.Command.UpdateJob;
using JobApplication.Application.Features.Command.CloseJob;
using JobApplication.Application.Features.Query.Jobs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class JobsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JobsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll( CancellationToken cancellationToken)
        {
            var result = await _mediator.Send( new GetAllJobsQuery(), cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Recruiter")]
        public async Task<IActionResult> CreateJob( CreateJobDto dto, CancellationToken cancellationToken)
        {
            var jobId = await _mediator.Send(
                new CreateJobCommand
                {
                    Title = dto.Title
                },
                cancellationToken);

            return Ok(new
            {
                message = "Job created successfully.",
                jobId
            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Recruiter")]
        public async Task<IActionResult> UpdateJob( int id, UpdateJobDto dto, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new UpdateJobCommand
                {
                    JobId = id,
                    Title = dto.Title
                },
                cancellationToken);

            if (!result)
                return NotFound(new
                {
                    message = "Job not found."
                });

            return Ok(new
            {
                message = "Job updated successfully."
            });
        }

        [HttpPut("{id}/close")]
        [Authorize(Roles = "Recruiter")]
        public async Task<IActionResult> CloseJob( int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new CloseJobCommand
                {
                    JobId = id
                },
                cancellationToken);

            if (!result)
                return NotFound(new
                {
                    message = "Job not found."
                });

            return Ok(new
            {
                message = "Job closed successfully."
            });
        }
    }
}
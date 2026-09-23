using JobApplication.Application.DTOs.Jobs;
using JobApplication.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using JobApplication.Application.Features.Command.CloseJob;
using JobApplication.Application.Features.Command.CreateJob;

namespace JobApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JobsController : ControllerBase
    {
        private readonly IMediator _iMediator;

        public JobsController(IMediator iMediator)
        {
            _iMediator = iMediator;
        }

        [HttpPut("{id}/close")]
        public async Task<IActionResult> CloseJob(int id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _iMediator.Send(new CloseJobCommand { JobId = id }, cancellationToken);

                if (!result)
                    return NotFound("Job not found.");

                return Ok("Job closed successfully.");
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateJob(CreateJobDto dto , CancellationToken cancellation)
        {
            try
            {
                var jobId = await _iMediator.Send(new CreateJobCommand { Title = dto.Title }, cancellation);

                return Ok(new
                {
                    message = "Job created successfully.",
                    jobId = jobId
                });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }
    }
}

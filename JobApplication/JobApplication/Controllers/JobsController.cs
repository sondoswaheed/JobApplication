using JobApplication.Application.DTOs.Jobs;
using JobApplication.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JobsController : ControllerBase
    {
        private readonly IJobService _jobService;

        public JobsController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpPut("{id}/close")]
        public async Task<IActionResult> CloseJob(int id)
        {
            try
            {
                var result = await _jobService.CloseJobAsync(id);

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
        public async Task<IActionResult> CreateJob(CreateJobDto dto)
        {
            try
            {
                var jobId = await _jobService.CreateJobAsync(dto.Title);

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

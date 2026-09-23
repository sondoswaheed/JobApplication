using JobApplication.Application.DTOs.Applications;
using JobApplication.Application.Features.Command.CancelApplication;
using JobApplication.Application.Features.Command.CreateApplication;
using JobApplication.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ApplicationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ApplicationsController(IMediator mediator)
        {
            _mediator= mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateApplication( CreateApplicationDto dto ,CancellationToken cancellation)
        {
            try
            {
                var applicationId = await _mediator.Send(new CreateApplicationCommand { CandidateId = dto.CandidateId , JobId=dto.JobId }, cancellation);

                return Ok(new
                {
                    message = "Application created successfully.",
                    applicationId = applicationId
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelApplication(int id ,CancellationToken cancellation)
        {
            try
            {
                var result = await _mediator.Send(new CancelApplicationCommand { id = id }, cancellation);

                if (!result)
                    return NotFound("Application not found.");

                return Ok("Application cancelled successfully.");
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
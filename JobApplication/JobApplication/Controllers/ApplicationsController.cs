using JobApplication.Application.DTOs.Applications;
using JobApplication.Application.Features.Command.CancelApplication;
using JobApplication.Application.Features.Command.CreateApplication;
using JobApplication.Application.Features.Query.GetAllApplications;
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
        [Authorize(Roles = "Candidate")]
        public async Task<IActionResult> CreateApplication( CreateApplicationDto dto ,CancellationToken cancellation)
        {
            try
            {
                var applicationId = await _mediator.Send(new CreateApplicationCommand {  JobId=dto.JobId }, cancellation);

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

        [HttpGet("my")]
        [Authorize(Roles = "Candidate")]
        public async Task<IActionResult> GetMyApplications(
           CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllApplicationsQuery(), cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Recruiter")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send( new GetAllApplicationsQuery(), cancellationToken);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Candidate")]
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
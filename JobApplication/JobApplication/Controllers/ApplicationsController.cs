using JobApplication.Application.DTOs.Applications;
using JobApplication.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationsController(
            IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateApplication(
            CreateApplicationDto dto)
        {
            try
            {
                var applicationId =
                    await _applicationService.CreateApplicationAsync(dto);

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
        public async Task<IActionResult> CancelApplication(int id)
        {
            try
            {
                var result =
                    await _applicationService.CancelApplicationAsync(id);

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
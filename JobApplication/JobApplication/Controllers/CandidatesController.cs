using JobApplication.Application.DTOs.Candidates;
using JobApplication.Application.Features.Command.UpdateCandidate;
using JobApplication.Application.Features.Query.GetAllCandidates;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CandidatesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CandidatesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Recruiter")]
        public async Task<IActionResult> GetAll( CancellationToken cancellationToken)
        {
            var result = await _mediator.Send( new GetAllCandidatesQuery(),cancellationToken);

            return Ok(result);
        }

        [HttpPut("me")]
        [Authorize(Roles = "Candidate")]
        public async Task<IActionResult> UpdateProfile(UpdateCandidatesDto dto, CancellationToken cancellationToken)
        {
            await _mediator.Send(
                new UpdateCandidateCommand
                {
                    Name = dto.Name,
                    CVUrl = dto.CVUrl
                },
                cancellationToken);

            return Ok(new
            {
                message = "Candidate profile updated successfully."
            });
        }
    }
}
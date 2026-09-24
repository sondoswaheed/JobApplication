using JobApplication.Application.DTOs.Candidates;
using MediatR;

namespace JobApplication.Application.Features.Query.GetAllCandidates
{
    public class GetAllCandidatesQuery : IRequest<List<CandidateResponseDto>>
    {
    }
}
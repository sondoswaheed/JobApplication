using JobApplication.Application.Interfaces.Repositories;
using JobApplication.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplication.Application.Features.Command.CreateCandidate
{
    public class CreateCandidateCommandHandler : IRequestHandler<CreateCandidateCommand, int>
    {
        private readonly ICandidateRepository _candidateRepository;

        public CreateCandidateCommandHandler(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }
        public async Task<int> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate = new Candidate
            {
                Name = request.Name,
                Email = request.Email,
                CVUrl = request.CVUrl
            };

            await _candidateRepository.AddAsync(candidate);

            return candidate.Id;
        }
    }
}

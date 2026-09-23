using JobApplication.Application.Interfaces;
using JobApplication.Application.Interfaces.Repositories;
using JobApplication.Domain.Entities;
using MediatR;

namespace JobApplication.Application.Features.Command.CreateApplication
{
    public class CreateApplicationCommandHandler : IRequestHandler<CreateApplicationCommand, int>
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly ICandidateRepository _candidateRepository;

        public CreateApplicationCommandHandler(IApplicationRepository applicationRepository, ICandidateRepository candidateRepository)
        {
            _applicationRepository = applicationRepository;
            _candidateRepository = candidateRepository;
        }
        public async Task<int> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _candidateRepository.GetByIdAsync(request.CandidateId);

            if (candidate == null)
                throw new InvalidOperationException(
                    "Candidate not found.");

            var application = new Applicationn
            {
                CandidateId = request.CandidateId,
                JobId = request.JobId,
                Status = "Applied",
                AppliedAt = DateTime.UtcNow
            };

            await _applicationRepository.AddAsync(application);

            return application.Id;
        }
    }
}

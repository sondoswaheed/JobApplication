using MediatR;

namespace JobApplication.Application.Features.Command.CancelApplication
{
    public class CancelApplicationCommand :IRequest<bool>
    {
        public int id {  get; set; }
    }
}

using ContactsAPI.Application.Reports.Commands;
using MassTransit;
using MediatR;
using SharedLibrary.Messages;
using System.Threading.Tasks;

namespace ContactsAPI.Application.Communications
{
    public class ReportResultMessageConsumer : IConsumer<ReportResultMessage>
    {
        private readonly IMediator mediator;

        public ReportResultMessageConsumer(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task Consume(ConsumeContext<ReportResultMessage> context)
        {
            await mediator.Send(new ReportReadyCommand()
            {
                ReportId = context.Message.ReportId
            });
        }
    }
}

using MassTransit;
using SharedLibrary.Domains;
using SharedLibrary.Messages;
using System;
using System.Threading.Tasks;

namespace ContactsAPI.Application.Communications
{
    public class MassTransitHelper : IMassTransitHelper
    {
        private readonly IPublishEndpoint endpoint;

        public MassTransitHelper(IPublishEndpoint endpoint)
        {
            this.endpoint = endpoint;
        }

        public async Task ReportPrepared(ReportDTO report)
        {
            await endpoint.Publish<ReportResultMessage>(new ReportResultMessage
            { 
                ReportId = report.ReportId,
                ResultState = true,
                ResultMessage = "İşlem Başarılı"
            });
        }
    }
}

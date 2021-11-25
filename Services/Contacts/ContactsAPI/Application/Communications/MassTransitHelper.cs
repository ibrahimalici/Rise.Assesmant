using MassTransit;
using SharedLibrary.Domains;
using SharedLibrary.Messages;
using System;
using System.Threading.Tasks;

namespace ContactsAPI.Application.Communications
{
    public class MassTransitHelper
    {
        private readonly ISendEndpointProvider provider;

        public MassTransitHelper(ISendEndpointProvider provider)
        {
            this.provider = provider;
        }

        public async Task PrepareReport(ReportDTO report)
        {
            var endPoint = await provider.GetSendEndpoint(new Uri("queue:report-integration"));
            await endPoint.Send(report);
        }
    }
}

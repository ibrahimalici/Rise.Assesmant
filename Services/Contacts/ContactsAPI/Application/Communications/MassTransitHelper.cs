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

        public async Task PrepareReport(ReportDTO report)
        {
            await endpoint.Publish<ReportMessage>(new ReportMessage
            { 
                Report = report 
            });
        }
    }
}

using MassTransit;
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

        public async Task SendKisi(KisiDTO kisi)
        {
            var endPoint = await provider.GetSendEndpoint(new Uri("queue:report-integration"));
            var data = new IntegrationMessage()
            {
                Kisi = kisi
            };

            await endPoint.Send(data);
        }

        public async Task DeleteKisi(Guid kisiId)
        {
            var endPoint = await provider.GetSendEndpoint(new Uri("queue:report-integration"));
            var data = new DeleteKisiMessage { DeleteKisiId = kisiId };
            await endPoint.Send(data);
        }
    }
}

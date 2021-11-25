using MassTransit;
using ReportsAPI.Data;
using SharedLibrary.Messages;
using System.Threading.Tasks;

namespace ReportsAPI.Application.Communications
{
    public class IntegrationMessageComsumer : IConsumer<IntegrationMessage>
    {
        private readonly IProductRepository repository;

        public IntegrationMessageComsumer(IProductRepository repository)
        {
            this.repository = repository;
        }

        public async Task Consume(ConsumeContext<IntegrationMessage> context)
        {
            await repository.SaveKisi(context.Message.Kisi);
        }
    }
}

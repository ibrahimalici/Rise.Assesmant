using MassTransit;
using ReportsAPI.Data;
using SharedLibrary.Messages;
using System.Threading.Tasks;

namespace ReportsAPI.Application.Communications
{
    public class DeleteKisiMessageConsumer : IConsumer<DeleteKisiMessage>
    {
        private readonly IProductRepository repository;

        public async Task Consume(ConsumeContext<DeleteKisiMessage> context)
        {
            await repository.DeleteKisi(context.Message.DeleteKisiId);
        }
    }
}

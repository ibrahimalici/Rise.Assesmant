using MassTransit;
using ReportsAPI.Repositories;
using SharedLibrary.Messages;
using System.Threading.Tasks;

namespace ReportsAPI.Application.Communications
{
    public class ReportMessageConsumer : IConsumer<ReportMessage>
    {
        private readonly DataRepository db;

        public ReportMessageConsumer(DataRepository db)
        {
            this.db = db;
        }

        public async Task Consume(ConsumeContext<ReportMessage> context)
        {
            await db.PrepareReport(context.Message.Report);
        }
    }
}

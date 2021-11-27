using MassTransit;
using ReportsAPI.Data;
using SharedLibrary.Messages;
using System.Threading.Tasks;

namespace ReportsAPI.Application.Communications
{
    public class ReportMessageConsumer : IConsumer<ReportMessage>
    {
        private readonly IDataRepository db;

        public ReportMessageConsumer(IDataRepository db)
        {
            this.db = db;
        }

        public async Task Consume(ConsumeContext<ReportMessage> context)
        {
            await db.PrepareReport(context.Message.Report);
        }
    }
}

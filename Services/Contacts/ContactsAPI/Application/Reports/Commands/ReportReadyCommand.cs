using ContactsAPI.Entities;
using ContactsAPI.Persistance;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ContactsAPI.Application.Reports.Commands
{
    public class ReportReadyCommand : IRequest<bool>
    {
        public Guid ReportId { get; set; }
    }

    public class ReportReadyCommandHandler :IRequestHandler<ReportReadyCommand,bool>
    {
        private readonly DatabaseContext db;

        public ReportReadyCommandHandler(DatabaseContext db)
        {
            this.db = db;
        }

        public async Task<bool> Handle(ReportReadyCommand request, CancellationToken cancellationToken)
        {
            Report report = await db.Reports.FindAsync(request.ReportId);
            report.ReportStatus = SharedLibrary.Domains.ReportStatus.Ready;
            await db.SaveChangesAsync();
            return true;
        }
    }
}

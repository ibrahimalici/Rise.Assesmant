using ContactsAPI.Entities;
using ContactsAPI.Persistance;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ContactsAPI.Application.Kisiler.Commands
{
    public class DeleteIletisimCommand : IRequest<bool>
    {
        public Guid KisiId { get; set; }
    }

    public class DeleteKisiHandle : IRequestHandler<DeleteIletisimCommand, bool>
    {
        private readonly DatabaseContext db;

        public DeleteKisiHandle(DatabaseContext db)
        {
            this.db = db;
        }

        public async Task<bool> Handle(DeleteIletisimCommand request, CancellationToken cancellationToken)
        {
            Kisi saved = await db.Kisiler.FindAsync(request.KisiId);
            db.Kisiler.Remove(saved);
            await db.SaveChangesAsync();
            return true;
        }
    }
}

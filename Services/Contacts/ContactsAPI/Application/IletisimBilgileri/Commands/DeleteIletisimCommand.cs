using ContactsAPI.Entities;
using ContactsAPI.Persistance;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ContactsAPI.Application.IletisimBilgileri.Commands
{
    public class DeleteIletisimCommand : IRequest<bool>
    {
        public Guid IletisimId { get; set; }
    }

    public class DeleteIletisimHandle : IRequestHandler<DeleteIletisimCommand, bool>
    {
        private readonly DatabaseContext db;

        public DeleteIletisimHandle(DatabaseContext db)
        {
            this.db = db;
        }

        public async Task<bool> Handle(DeleteIletisimCommand request, CancellationToken cancellationToken)
        {
            Iletisim saved = await db.IletisimBilgileri.FindAsync(request.IletisimId);
            db.IletisimBilgileri.Remove(saved);
            await db.SaveChangesAsync();
            return true;
        }
    }
}

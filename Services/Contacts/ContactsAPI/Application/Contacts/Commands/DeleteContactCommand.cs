using ContactsAPI.Entities;
using ContactsAPI.Persistance;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ContactsAPI.Application.ContactsInfo.Commands
{
    public class DeleteContactCommand : IRequest<bool>
    {
        public Guid KisiId { get; set; }
    }

    public class DeleteContactHandler : IRequestHandler<DeleteContactCommand, bool>
    {
        private readonly DatabaseContext db;

        public DeleteContactHandler(DatabaseContext db)
        {
            this.db = db;
        }

        public async Task<bool> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            Contact saved = await db.Contacts.FindAsync(request.KisiId);
            db.Contacts.Remove(saved);
            await db.SaveChangesAsync();
            return true;
        }
    }
}

using ContactsAPI.Domains;
using ContactsAPI.Entities;
using ContactsAPI.Persistance;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ContactsAPI.Application.IletisimBilgileri.Commands
{
    public class UpdateIletisimCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public ContactDetailType BilgiTipi { get; set; }
        public string BilgiIcerigi { get; set; }
        public Guid KisiId { get; set; }
    }

    public class UpdateIletisimHandle : IRequestHandler<UpdateIletisimCommand,bool>
    {
        private readonly DatabaseContext db;

        public UpdateIletisimHandle(DatabaseContext db)
        {
            this.db = db;
        }

        public async Task<bool> Handle(UpdateIletisimCommand request, CancellationToken cancellationToken)
        {
            Iletisim saved = await db.IletisimBilgileri.FindAsync(request.Id);

            saved.BilgiTipi = request.BilgiTipi;
            saved.BilgiIcerigi = request.BilgiIcerigi;

            return true;
        }
    }
}

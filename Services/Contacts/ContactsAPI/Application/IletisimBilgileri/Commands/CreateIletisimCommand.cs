using ContactsAPI.Domains;
using ContactsAPI.Entities;
using ContactsAPI.Persistance;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ContactsAPI.Application.IletisimBilgileri.Commands
{
    public class CreateIletisimCommand : IRequest<Guid>
    {
        public ContactDetailType BilgiTipi { get; set; }
        public string BilgiIcerigi { get; set; }
        public Guid KisiId { get; set; }
    }

    public class CreateIletisimHandle : IRequestHandler<CreateIletisimCommand, Guid>
    {
        private readonly DatabaseContext db;

        public CreateIletisimHandle(DatabaseContext db)
        {
            this.db = db;
        }

        public async Task<Guid> Handle(CreateIletisimCommand request, CancellationToken cancellationToken)
        {
            Iletisim data = new Iletisim
            {
                Id= Guid.NewGuid(),
                KisiId = request.KisiId,
                BilgiTipi = request.BilgiTipi,
                BilgiIcerigi = request.BilgiIcerigi
            };
            await db.IletisimBilgileri.AddAsync(data);
            await db.SaveChangesAsync();
            return data.Id;
        }
    }
}

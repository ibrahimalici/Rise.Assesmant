using ContactsAPI.Entities;
using ContactsAPI.Persistance;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ContactsAPI.Application.Kisiler.Commands
{
    public class CreateIletisimCommand : IRequest<Guid>
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Firma { get; set; }
    }

    public class CreateKisiHandle : IRequestHandler<CreateIletisimCommand, Guid>
    {
        private readonly DatabaseContext db;

        public CreateKisiHandle(DatabaseContext db)
        {
            this.db = db;
        }

        public async Task<Guid> Handle(CreateIletisimCommand request, CancellationToken cancellationToken)
        {
            Kisi data = new Kisi
            {
                Ad = request.Ad,
                Soyad = request.Soyad,
                Firma = request.Firma,
                Id = Guid.NewGuid()
            };
            await db.Kisiler.AddAsync(data);
            await db.SaveChangesAsync();
            return data.Id;
        }
    }
}

using ContactsAPI.Entities;
using ContactsAPI.Persistance;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ContactsAPI.Application.Kisiler.Commands
{
    public class UpdateIletisimCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Firma { get; set; }
    }

    public class UpdateKisiHandle : IRequestHandler<UpdateIletisimCommand,bool>
    {
        private readonly DatabaseContext db;

        public UpdateKisiHandle(DatabaseContext db)
        {
            this.db = db;
        }

        public async Task<bool> Handle(UpdateIletisimCommand request, CancellationToken cancellationToken)
        {
            Kisi saved = await db.Kisiler.FindAsync(request.Id);

            saved.Ad = request.Ad;
            saved.Firma =request.Firma;
            saved.Soyad =request.Soyad;
            await db.SaveChangesAsync();

            return true;
        }
    }
}

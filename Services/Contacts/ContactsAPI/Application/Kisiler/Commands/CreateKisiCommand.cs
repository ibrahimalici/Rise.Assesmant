using AutoMapper;
using ContactsAPI.Application.Communications;
using ContactsAPI.Entities;
using ContactsAPI.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Domains;
using SharedLibrary.Messages;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ContactsAPI.Application.Kisiler.Commands
{
    public class CreateKisiCommand : IRequest<Guid>
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Firma { get; set; }
    }

    public class CreateKisiHandle : IRequestHandler<CreateKisiCommand, Guid>
    {
        private readonly DatabaseContext db;

        public CreateKisiHandle(DatabaseContext db)
        {
            this.db = db;
        }

        public async Task<Guid> Handle(CreateKisiCommand request, CancellationToken cancellationToken)
        {
            Contact data = new Contact
            {
                Ad = request.Ad,
                Soyad = request.Soyad,
                Firma = request.Firma,
                ContactId = Guid.NewGuid()
            };
            await db.Contacts.AddAsync(data);
            await db.SaveChangesAsync();

            return data.ContactId;
        }
    }
}

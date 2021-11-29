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

namespace ContactsAPI.Application.ContactsInfo.Commands
{
    public class CreateContactCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Surename { get; set; }
        public string Company { get; set; }
    }

    public class CreateContactHandle : IRequestHandler<CreateContactCommand, Guid>
    {
        private readonly DatabaseContext db;

        public CreateContactHandle(DatabaseContext db)
        {
            this.db = db;
        }

        public async Task<Guid> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            Contact data = new Contact
            {
                Name = request.Name,
                Surename = request.Surename,
                Company = request.Company,
                ContactId = Guid.NewGuid()
            };
            await db.Contacts.AddAsync(data);
            await db.SaveChangesAsync();

            return data.ContactId;
        }
    }
}

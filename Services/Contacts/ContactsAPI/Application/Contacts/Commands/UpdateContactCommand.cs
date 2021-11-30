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
    public class UpdateContactCommand : IRequest<bool>
    {
        public Guid ContactId { get; set; }
        public string Name { get; set; }
        public string Surename { get; set; }
        public string Company { get; set; }
    }

    public class UpdateContactHandler : IRequestHandler<UpdateContactCommand, bool>
    {
        private readonly DatabaseContext db;

        public UpdateContactHandler(DatabaseContext db, IMapper mapper)
        {
            this.db = db;
        }

        public async Task<bool> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            Contact saved = await db.Contacts.FindAsync(request.ContactId);

            saved.Name = request.Name;
            saved.Company =request.Company;
            saved.Surename =request.Surename;
            await db.SaveChangesAsync();

            return true;
        }
    }
}

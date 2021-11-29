using AutoMapper;
using ContactsAPI.Application.Communications;
using ContactsAPI.Entities;
using ContactsAPI.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Domains;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ContactsAPI.Application.ContactSubDetails.Commands
{
    public class UpdateContactDetailCommand : IRequest<bool>
    {
        public Guid ContactDetailId { get; set; }
        public ContactDetailType ContactDetailType { get; set; }
        public string Description { get; set; }
        public Guid ContactId { get; set; }
    }

    public class UpdateContactDetailHandle : IRequestHandler<UpdateContactDetailCommand,bool>
    {
        private readonly DatabaseContext db;

        public UpdateContactDetailHandle(DatabaseContext db)
        {
            this.db = db;
        }

        public async Task<bool> Handle(UpdateContactDetailCommand request, CancellationToken cancellationToken)
        {
            ContactDetail saved = await db.ContactDetails.FindAsync(request.ContactDetailId);

            saved.BilgiTipi = request.ContactDetailType;
            saved.BilgiIcerigi = request.Description;
            await db.SaveChangesAsync();

            return true;
        }
    }
}

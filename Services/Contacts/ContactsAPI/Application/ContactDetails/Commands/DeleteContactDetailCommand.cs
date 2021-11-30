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

namespace ContactsAPI.Application.ContactSubDetails.Commands
{
    public class DeleteContactDetailCommand : IRequest<bool>
    {
        public Guid ContactDetailId { get; set; }
    }

    public class DeleteContactDetailHandler : IRequestHandler<DeleteContactDetailCommand, bool>
    {
        private readonly DatabaseContext db;

        public DeleteContactDetailHandler(DatabaseContext db)
        {
            this.db = db;
        }

        public async Task<bool> Handle(DeleteContactDetailCommand request, CancellationToken cancellationToken)
        {
            ContactDetail saved = await db.ContactDetails.FindAsync(request.ContactDetailId);
            db.ContactDetails.Remove(saved);
            await db.SaveChangesAsync();

            return true;
        }
    }
}

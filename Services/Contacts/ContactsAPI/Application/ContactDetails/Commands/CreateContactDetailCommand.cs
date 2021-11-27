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
    public class CreateContactDetailCommand : IRequest<Guid>
    {
        public ContactDetailType ContactDetailType { get; set; }
        public string Description { get; set; }
        public Guid ContactId { get; set; }
    }

    public class CreateContactDetailHandle : IRequestHandler<CreateContactDetailCommand, Guid>
    {
        private readonly DatabaseContext db;

        public CreateContactDetailHandle(DatabaseContext db, MassTransitHelper queueHelper, IMapper mapper)
        {
            this.db = db;
        }

        public async Task<Guid> Handle(CreateContactDetailCommand request, CancellationToken cancellationToken)
        {
            ContactDetail data = new ContactDetail
            {
                ContactDetailId= Guid.NewGuid(),
                KisiId = request.ContactId,
                BilgiTipi = request.ContactDetailType,
                BilgiIcerigi = request.Description
            };
            await db.ContactDetails.AddAsync(data);
            await db.SaveChangesAsync();
            return data.ContactDetailId;
        }
    }
}

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

        public CreateIletisimHandle(DatabaseContext db, MassTransitHelper queueHelper, IMapper mapper)
        {
            this.db = db;
        }

        public async Task<Guid> Handle(CreateIletisimCommand request, CancellationToken cancellationToken)
        {
            ContactDetail data = new ContactDetail
            {
                ContactDetailId= Guid.NewGuid(),
                KisiId = request.KisiId,
                BilgiTipi = request.BilgiTipi,
                BilgiIcerigi = request.BilgiIcerigi
            };
            await db.ContactDetails.AddAsync(data);
            await db.SaveChangesAsync();
            return data.ContactDetailId;
        }
    }
}

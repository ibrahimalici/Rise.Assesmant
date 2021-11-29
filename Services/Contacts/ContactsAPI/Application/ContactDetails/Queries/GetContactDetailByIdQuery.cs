using AutoMapper;
using ContactsAPI.Entities;
using ContactsAPI.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Domains;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ContactsAPI.Application.ContactSubDetails.Queries
{
    public class GetContactDetailByIdQuery : IRequest<ContactDetailDTO>
    {
        public Guid ContactDetailId { get; set; }
    }

    public class GetContactDetailByIdHandle : IRequestHandler<GetContactDetailByIdQuery, ContactDetailDTO>
    {
        private readonly DatabaseContext db;
        private readonly IMapper mapper;

        public GetContactDetailByIdHandle(DatabaseContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<ContactDetailDTO> Handle(GetContactDetailByIdQuery request, CancellationToken cancellationToken)
        {
            ContactDetail Iletisim = await db.ContactDetails.Include(o => o.Contact).FirstOrDefaultAsync(x=>x.ContactDetailId == request.ContactDetailId);
            ContactDetailDTO result = mapper.Map<ContactDetailDTO>(Iletisim);
            return result;
        }
    }
}

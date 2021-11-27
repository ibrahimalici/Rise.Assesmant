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
    public class GetIletisimByIdQuery : IRequest<ContactDetailsDTO>
    {
        public Guid IletisimId { get; set; }
    }

    public class GetIletisimByIdHandle : IRequestHandler<GetIletisimByIdQuery, ContactDetailsDTO>
    {
        private readonly DatabaseContext db;
        private readonly IMapper mapper;

        public GetIletisimByIdHandle(DatabaseContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<ContactDetailsDTO> Handle(GetIletisimByIdQuery request, CancellationToken cancellationToken)
        {
            ContactDetail Iletisim = await db.ContactDetails.Include(o => o.Kisi).FirstOrDefaultAsync(x=>x.ContactDetailId == request.IletisimId);
            ContactDetailsDTO result = mapper.Map<ContactDetailsDTO>(Iletisim);
            return result;
        }
    }
}

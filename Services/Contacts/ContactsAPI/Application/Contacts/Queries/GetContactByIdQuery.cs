using AutoMapper;
using ContactsAPI.Entities;
using ContactsAPI.Persistance;
using MediatR;
using SharedLibrary.Domains;
using SharedLibrary.Messages;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ContactsAPI.Application.ContactsInfo.Queries
{
    public class GetContactByIdQuery : IRequest<ContactDTO>
    {
        public Guid ContactId { get; set; }
    }

    public class GetContactByIdHandle : IRequestHandler<GetContactByIdQuery, ContactDTO>
    {
        private readonly DatabaseContext db;
        private readonly IMapper mapper;

        public GetContactByIdHandle(DatabaseContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<ContactDTO> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
        {
            Contact kisi = await db.Contacts.FindAsync(request.ContactId);
            ContactDTO result = mapper.Map<ContactDTO>(kisi);
            return result;
        }
    }
}

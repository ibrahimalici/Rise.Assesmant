using AutoMapper;
using ContactsAPI.Entities;
using ContactsAPI.Persistance;
using MediatR;
using SharedLibrary.Domains;
using SharedLibrary.Messages;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ContactsAPI.Application.Kisiler.Queries
{
    public class GetKisiByIdQuery : IRequest<ContactsDTO>
    {
        public Guid KisiId { get; set; }
    }

    public class GetKisiByIdHandle : IRequestHandler<GetKisiByIdQuery, ContactsDTO>
    {
        private readonly DatabaseContext db;
        private readonly IMapper mapper;

        public GetKisiByIdHandle(DatabaseContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<ContactsDTO> Handle(GetKisiByIdQuery request, CancellationToken cancellationToken)
        {
            Contact kisi = await db.Contacts.FindAsync(request.KisiId);
            ContactsDTO result = mapper.Map<ContactsDTO>(kisi);
            return result;
        }
    }
}

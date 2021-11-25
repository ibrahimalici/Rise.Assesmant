using AutoMapper;
using ContactsAPI.Entities;
using ContactsAPI.Persistance;
using MediatR;
using SharedLibrary.Messages;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ContactsAPI.Application.Kisiler.Queries
{
    public class GetKisiByIdQuery : IRequest<KisiDTO>
    {
        public Guid KisiId { get; set; }
    }

    public class GetKisiByIdHandle : IRequestHandler<GetKisiByIdQuery, KisiDTO>
    {
        private readonly DatabaseContext db;
        private readonly IMapper mapper;

        public GetKisiByIdHandle(DatabaseContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<KisiDTO> Handle(GetKisiByIdQuery request, CancellationToken cancellationToken)
        {
            Kisi kisi = await db.Kisiler.FindAsync(request.KisiId);
            KisiDTO result = mapper.Map<KisiDTO>(kisi);
            return result;
        }
    }
}

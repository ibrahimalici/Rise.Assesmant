using AutoMapper;
using ContactsAPI.Domains;
using ContactsAPI.Entities;
using ContactsAPI.Persistance;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ContactsAPI.Application.IletisimBilgileri.Queries
{
    public class GetIletisimByIdQuery : IRequest<IletisimDTO>
    {
        public Guid IletisimId { get; set; }
    }

    public class GetIletisimByIdHandle : IRequestHandler<GetIletisimByIdQuery, IletisimDTO>
    {
        private readonly DatabaseContext db;
        private readonly IMapper mapper;

        public GetIletisimByIdHandle(DatabaseContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<IletisimDTO> Handle(GetIletisimByIdQuery request, CancellationToken cancellationToken)
        {
            Iletisim Iletisim = await db.IletisimBilgileri.FindAsync(request.IletisimId);
            IletisimDTO result = mapper.Map<IletisimDTO>(Iletisim);
            return result;
        }
    }
}

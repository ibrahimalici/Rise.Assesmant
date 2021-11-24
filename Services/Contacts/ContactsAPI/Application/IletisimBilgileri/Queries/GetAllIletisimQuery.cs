using AutoMapper;
using ContactsAPI.Domains;
using ContactsAPI.Persistance;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ContactsAPI.Entities;

namespace ContactsAPI.Application.IletisimBilgileri.Queries
{
    public class GetAllIletisimQuery : IRequest<List<IletisimDTO>>
    {
        public bool Paging { get; set; }
        public int StartIndex { get; set; }
        public int RecordCount { get; set; }
    }

    public class GetAllIletisimHandle : IRequestHandler<GetAllIletisimQuery, List<IletisimDTO>>
    {
        private readonly DatabaseContext db;
        private readonly IMapper mapper;

        public GetAllIletisimHandle(DatabaseContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<List<IletisimDTO>> Handle(GetAllIletisimQuery request, CancellationToken cancellationToken)
        {
            List<Iletisim> data = new List<Iletisim>();

            if (!request.Paging)
                data = db.IletisimBilgileri.ToList();
            else
            {
                db.IletisimBilgileri.Skip(request.StartIndex).Take(request.RecordCount).ToList();
            }

            List<IletisimDTO> result = mapper.Map<List<IletisimDTO>>(data);
            return result;
        }
    }
}

using AutoMapper;
using ContactsAPI.Domains;
using ContactsAPI.Entities;
using ContactsAPI.Persistance;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace ContactsAPI.Application.Kisiler.Queries
{
    public class GetAllIletisimQuery : IRequest<List<KisiDTO>>
    {
        public bool Paging { get; set; }
        public int StartIndex { get; set; }
        public int RecordCount { get; set; }
    }

    public class GetAllKisilerHandle : IRequestHandler<GetAllIletisimQuery, List<KisiDTO>>
    {
        private readonly DatabaseContext db;
        private readonly IMapper mapper;

        public GetAllKisilerHandle(DatabaseContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<List<KisiDTO>> Handle(GetAllIletisimQuery request, CancellationToken cancellationToken)
        {
            List<Kisi> data = new List<Kisi>();

            if (!request.Paging)
                data = db.Kisiler.ToList();
            else
            {
                db.Kisiler.Skip(request.StartIndex).Take(request.RecordCount).ToList();
            }

            List<KisiDTO> result = mapper.Map<List<KisiDTO>>(data);
            return result;
        }
    }
}

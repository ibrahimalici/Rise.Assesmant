using AutoMapper;
using ContactsAPI.Entities;
using ContactsAPI.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContactsAPI.Application.IletisimBilgileri.Queries
{
    public class GetAllIletisimQuery : IRequest<List<IletisimDTO>>
    {
        public Guid? KisiId { get; set; }
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
                data = db.IletisimBilgileri.Include(o => o.Kisi)
                    .Where(p => !request.KisiId.HasValue || request.KisiId.ToString().Contains("00000") || p.KisiId == request.KisiId.Value)
                    .ToList();
            else
            {
                data = db.IletisimBilgileri.Include(o=>o.Kisi)
                    .Where(p=> !request.KisiId.HasValue || request.KisiId.ToString().Contains("00000") || p.KisiId == request.KisiId.Value)
                    .Skip(request.StartIndex).Take(request.RecordCount).ToList();
            }

            List<IletisimDTO> result = mapper.Map<List<IletisimDTO>>(data);
            return result;
        }
    }
}

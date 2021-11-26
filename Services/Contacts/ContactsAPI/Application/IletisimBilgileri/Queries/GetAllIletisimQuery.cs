using AutoMapper;
using ContactsAPI.Entities;
using ContactsAPI.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContactsAPI.Application.IletisimBilgileri.Queries
{
    public class GetAllIletisimQuery : IRequest<List<ContactDetailsDTO>>
    {
        public Guid? KisiId { get; set; }
        public bool Paging { get; set; }
        public int StartIndex { get; set; }
        public int RecordCount { get; set; }
    }

    public class GetAllIletisimHandle : IRequestHandler<GetAllIletisimQuery, List<ContactDetailsDTO>>
    {
        private readonly DatabaseContext db;
        private readonly IMapper mapper;

        public GetAllIletisimHandle(DatabaseContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<List<ContactDetailsDTO>> Handle(GetAllIletisimQuery request, CancellationToken cancellationToken)
        {
            List<ContactDetail> data = new List<ContactDetail>();

            if (!request.Paging)
                data = db.ContactDetails.Include(o => o.Kisi)
                    .Where(p => !request.KisiId.HasValue || request.KisiId.ToString().Contains("00000") || p.KisiId == request.KisiId.Value)
                    .ToList();
            else
            {
                data = db.ContactDetails.Include(o=>o.Kisi)
                    .Where(p=> !request.KisiId.HasValue || request.KisiId.ToString().Contains("00000") || p.KisiId == request.KisiId.Value)
                    .Skip(request.StartIndex).Take(request.RecordCount).ToList();
            }

            List<ContactDetailsDTO> result = mapper.Map<List<ContactDetailsDTO>>(data);
            return result;
        }
    }
}

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

namespace ContactsAPI.Application.ContactSubDetails.Queries
{
    public class GetAllContactDetailsQuery : IRequest<List<ContactDetailDTO>>
    {
        public Guid? ContactId { get; set; }
        public bool Paging { get; set; }
        public int StartIndex { get; set; }
        public int RecordCount { get; set; }
    }

    public class GetAllContactDetailHandler : IRequestHandler<GetAllContactDetailsQuery, List<ContactDetailDTO>>
    {
        private readonly DatabaseContext db;
        private readonly IMapper mapper;

        public GetAllContactDetailHandler(DatabaseContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<List<ContactDetailDTO>> Handle(GetAllContactDetailsQuery request, CancellationToken cancellationToken)
        {
            List<ContactDetail> data = new List<ContactDetail>();

            if (!request.Paging)
                data = db.ContactDetails.Include(o => o.Contact)
                    .Where(p => !request.ContactId.HasValue || request.ContactId.ToString().Contains("00000") || p.ContactId == request.ContactId.Value)
                    .ToList();
            else
            {
                data = db.ContactDetails.Include(o=>o.Contact)
                    .Where(p=> !request.ContactId.HasValue || request.ContactId.ToString().Contains("00000") || p.ContactId == request.ContactId.Value)
                    .Skip(request.StartIndex).Take(request.RecordCount).ToList();
            }

            List<ContactDetailDTO> result = mapper.Map<List<ContactDetailDTO>>(data);
            return result;
        }
    }
}

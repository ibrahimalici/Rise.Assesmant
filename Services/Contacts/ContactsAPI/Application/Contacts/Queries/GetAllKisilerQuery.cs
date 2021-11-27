using AutoMapper;
using ContactsAPI.Entities;
using ContactsAPI.Persistance;
using MediatR;
using SharedLibrary.Domains;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace ContactsAPI.Application.ContactsInfo.Queries
{
    public class GetAllKisilerQuery : IRequest<List<ContactsDTO>>
    {
        public bool Paging { get; set; }
        public int StartIndex { get; set; }
        public int RecordCount { get; set; }
    }

    public class GetAllKisilerHandle : IRequestHandler<GetAllKisilerQuery, List<ContactsDTO>>
    {
        private readonly DatabaseContext db;
        private readonly IMapper mapper;

        public GetAllKisilerHandle(DatabaseContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<List<ContactsDTO>> Handle(GetAllKisilerQuery request, CancellationToken cancellationToken)
        {
            List<Contact> data = new List<Contact>();

            if (!request.Paging)
                data = db.Contacts.ToList();
            else
            {
                data = db.Contacts.Skip(request.StartIndex).Take(request.RecordCount).ToList();
            }

            List<ContactsDTO> result = mapper.Map<List<ContactsDTO>>(data);
            return result;
        }
    }
}

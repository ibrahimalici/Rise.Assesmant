using AutoMapper;
using ContactsAPI.Entities;
using ContactsAPI.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Domains;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ContactsAPI.Application.ReportsInfo.Queries
{
    public class GetAllReportQuery : IRequest<List<ReportDTO>>
    {
        
    }

    public class GetAllReportHandle : IRequestHandler<GetAllReportQuery, List<ReportDTO>>
    {
        private readonly DatabaseContext db;
        private readonly IMapper mapper;

        public GetAllReportHandle(DatabaseContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<List<ReportDTO>> Handle(GetAllReportQuery request, CancellationToken cancellationToken)
        {
            List<Report> data = await db.Reports.ToListAsync();
            List<ReportDTO> result = mapper.Map<List<ReportDTO>>(data);
            return result;
        }
    }
}

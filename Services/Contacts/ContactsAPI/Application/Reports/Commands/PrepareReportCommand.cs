using ContactsAPI.Entities;
using ContactsAPI.Persistance;
using MediatR;
using SharedLibrary.Domains;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using System.Collections.Generic;
using ContactsAPI.Application.Communications;
using SharedLibrary.Messages;

namespace ContactsAPI.Application.Reports.Commands
{
    public class PrepareReportCommand : IRequest<ReportDTO>
    {
    }

    public class PrepareReportHandle : IRequestHandler<PrepareReportCommand, ReportDTO>
    {
        private readonly DatabaseContext db;
        private readonly IMapper mapper;
        private readonly IMassTransitHelper integration;
        public PrepareReportHandle(DatabaseContext db, IMapper mapper, IMassTransitHelper massTransitHelper)
        {
            this.db = db;
            this.mapper = mapper;
            this.integration = massTransitHelper;
        }

        public async Task<ReportDTO> Handle(PrepareReportCommand request, CancellationToken cancellationToken)
        {
            Report report = new Report();
            report.ReportId = Guid.NewGuid();
            report.ReportDemandDateTime = DateTime.Now;
            report.ReportStatus = ReportStatus.Preparing;
            await db.Reports.AddAsync(report);
            await db.SaveChangesAsync();

            ReportDTO messageObject = mapper.Map<ReportDTO>(report);

            var result1 = from p in db.ContactDetails
                          join q in db.Contacts on p.ContactId equals q.ContactId
                          where p.ContactDetailType == ContactDetailType.Location
                          group p by p.Description into grouped
                          select new
                          {
                              grouped.Key,
                              kisiSayisi = grouped.Count(),
                          };

            var result2 = from p in db.ContactDetails
                          join q in db.ContactDetails on p.ContactId equals q.ContactId
                          where p.ContactDetailType == ContactDetailType.Location && q.ContactDetailType == ContactDetailType.PhoneNumber
                          group p by p.Description into grouped
                          select new
                          {
                              grouped.Key,
                              telSayisi = grouped.Count(),
                          };

            List<ReportDetailDTO> result = (from p in result1
                                            join q in result2 on p.Key equals q.Key
                                            select new ReportDetailDTO
                                            {
                                                Location = p.Key,
                                                ContactCount = p.kisiSayisi,
                                                PhoneCount = q.telSayisi,
                                                ReportId = report.ReportId
                                            }).ToList();

            messageObject.Reports = result;
            await integration.PrepareReport(messageObject);

            ReportDTO reportMain = mapper.Map<ReportDTO>(report);
            return reportMain;
        }
    }
}

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

namespace ContactsAPI.Application.Reports.Commands
{
    public class PrepareReportCommand : IRequest<ReportDTO>
    {
    }

    public class PrepareReportHandle : IRequestHandler<PrepareReportCommand, ReportDTO>
    {
        private readonly DatabaseContext db;
        private readonly IMapper mapper;
        private readonly MassTransitHelper massTransitHelper;

        public PrepareReportHandle(DatabaseContext db, IMapper mapper, MassTransitHelper massTransitHelper)
        {
            this.db = db;
            this.mapper = mapper;
            this.massTransitHelper = massTransitHelper;
        }

        public async Task<ReportDTO> Handle(PrepareReportCommand request, CancellationToken cancellationToken)
        {
            Report report = new Report();
            report.Id = Guid.NewGuid();
            report.RaporTalepTarihi = DateTime.Now;
            report.RaporDurumu = ReportStatus.Hazirlaniyor;
            await db.Raporlar.AddAsync(report);

            ReportDTO messageObject = mapper.Map<ReportDTO>(report);

            var result1 = from p in db.IletisimBilgileri
                          join q in db.Kisiler on p.KisiId equals q.Id
                          where p.BilgiTipi == ContactDetailType.Konum
                          group p by p.BilgiIcerigi into grouped
                          select new
                          {
                              grouped.Key,
                              kisiSayisi = grouped.Count(),
                          };

            var result2 = from p in db.IletisimBilgileri
                          join q in db.IletisimBilgileri on p.KisiId equals q.KisiId
                          where p.BilgiTipi == ContactDetailType.Konum && q.BilgiTipi == ContactDetailType.TelNo
                          group p by p.BilgiIcerigi into grouped
                          select new
                          {
                              grouped.Key,
                              telSayisi = grouped.Count(),
                          };

            List<ReportDetailDTO> result = (from p in result1
                                            join q in result2 on p.Key equals q.Key
                                            select new ReportDetailDTO
                                            {
                                                KonumBilgisi = p.Key,
                                                KisiSayisi = p.kisiSayisi,
                                                TelSayisi = q.telSayisi
                                            }).ToList();

            messageObject.Reports = new List<ReportDetailDTO>();

            await massTransitHelper.PrepareReport(messageObject);

            return new ReportDTO
            {
                Id = messageObject.Id,
                RaporDurumu = ReportStatus.Hazirlaniyor,
                RaporTalepTarihi = messageObject.RaporTalepTarihi
            };
        }
    }
}

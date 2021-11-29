using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using ReportsAPI.Data;
using ReportsAPI.Entities;
using SharedLibrary.Domains;
using SharedLibrary.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportsAPI.Repositories
{
    public class DataRepository : IDataRepository
    {
        private readonly IConfiguration Configuration;
        private readonly MongoClient mongoClient = null;
        private readonly IMongoDatabase database = null;
        private readonly IMongoCollection<Report> reportTable = null;
        //private readonly IMongoCollection<ReportDetail> reportDetailTable = null;
        private readonly IMapper mapper = null;
        private readonly IPublishEndpoint publishEndpoint= null;

        public DataRepository(IConfiguration configuration, IMapper mapper, IPublishEndpoint endpoint)
        {
            this.Configuration = configuration;

            string mongoCnn = Configuration.GetValue<string>("DatabaseSettings:MongoConnectionString");
            string mongoReportsDb = Configuration.GetValue<string>("DatabaseSettings:MongoReportsDB");
            string mongoReportsTable = Configuration.GetValue<string>("DatabaseSettings:MongoReportsTable");
            string mongoReportDetailsTable = Configuration.GetValue<string>("DatabaseSettings:MongoReportDetailsTable");
            mongoClient = new MongoClient(mongoCnn);
            database = mongoClient.GetDatabase(mongoReportsDb);
            reportTable = database.GetCollection<Report>(mongoReportsTable);
            //reportDetailTable = database.GetCollection<ReportDetail>(mongoReportDetailsTable);

            this.mapper = mapper;
            this.publishEndpoint = endpoint;
        }

        public async Task<ReportDTO> GetReportObject(Guid id)
        {
            Report report = reportTable.Find(o => o.ReportId == id).FirstOrDefault();
            ReportDTO result = mapper.Map<ReportDTO>(report);
            return result;
        }

        public List<ReportDTO> GetAllReportObjects()
        {
            List<Report> reports = reportTable.AsQueryable().ToList();
            List<ReportDTO> result = mapper.Map<List<ReportDTO>>(reports);
            return result;
        }

        public async Task<bool> PrepareReport(ReportDTO report)
        {
            var new_ = mapper.Map<Report>(report);
            await reportTable.InsertOneAsync(new_);

            await publishEndpoint.Publish<ReportResultMessage>(new ReportResultMessage
            {
                ReportId = report.ReportId,
                ResultMessage = "İşlem Başarılı",
                ResultState = true
            });

            return true;
        }
    }
}

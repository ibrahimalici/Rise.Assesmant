using AutoMapper;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using ReportsAPI.Data;
using ReportsAPI.Entities;
using SharedLibrary.Domains;
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
        private readonly IMapper mapper = null;

        public DataRepository(IConfiguration configuration, IMapper mapper)
        {
            string mongoCnn = Configuration.GetValue<string>("DatabaseSettings:MongoConnectionString");
            string mongoProductDb = Configuration.GetValue<string>("DatabaseSettings:MongoContactsDB");
            string mongoProductsTable = Configuration.GetValue<string>("DatabaseSettings:MongoContactsTable");
            mongoClient = new MongoClient(mongoCnn);
            database = mongoClient.GetDatabase(mongoProductDb);
            reportTable = database.GetCollection<Report>(mongoProductsTable);

            this.mapper = mapper;
        }

        public async Task<ReportDTO> GetReportObject()
        {
            Report report = await reportTable.AsQueryable().FirstOrDefaultAsync();
            ReportDTO result = mapper.Map<ReportDTO>(report);
            return result;
        }

        public async Task<bool> PrepareReport(ReportDTO report)
        {
            var new_ = mapper.Map<Report>(report);
            await reportTable.ReplaceOneAsync(o => o.Id == new_.Id, new_);
            return true;
        }
    }
}

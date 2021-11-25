using AutoMapper;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using ReportsAPI.Data;
using ReportsAPI.Entities;
using SharedLibrary.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportsAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration Configuration;
        private readonly MongoClient mongoClient = null;
        private readonly IMongoDatabase database = null;
        private readonly IMongoCollection<Kisi> productsTable = null;
        private readonly IMapper mapper = null;

        public ProductRepository(IConfiguration configuration, IMapper mapper)
        {
            string mongoCnn = Configuration.GetValue<string>("DatabaseSettings:MongoConnectionString");
            string mongoProductDb = Configuration.GetValue<string>("DatabaseSettings:MongoContactsDB");
            string mongoProductsTable = Configuration.GetValue<string>("DatabaseSettings:MongoContactsTable");
            mongoClient = new MongoClient(mongoCnn);
            database = mongoClient.GetDatabase(mongoProductDb);
            productsTable = database.GetCollection<Kisi>(mongoProductsTable);

            this.mapper = mapper;
        }

        public async Task<List<KisiDTO>> GetReportObject()
        {
            List<Kisi> kisiler = await productsTable.AsQueryable().ToListAsync();
            List<KisiDTO> result = mapper.Map<List<KisiDTO>>(kisiler);
            return result;
        }

        public async Task<bool> SaveKisi(KisiDTO kisi)
        {
            var new_ = mapper.Map<Kisi>(kisi);
            await productsTable.ReplaceOneAsync(o => o.Id == new_.Id, new_);
            return true;
        }

        public async Task<bool> DeleteKisi(Guid kisiId)
        {
            await productsTable.DeleteOneAsync(o => o.Id == kisiId);
            return true;
        }
    }
}

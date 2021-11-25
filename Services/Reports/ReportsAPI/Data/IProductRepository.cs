using SharedLibrary.Messages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportsAPI.Data
{
    public interface IProductRepository
    {
        Task<List<KisiDTO>> GetReportObject();

        Task<bool> SaveKisi(KisiDTO kisi);

        Task<bool> DeleteKisi(Guid kisiId);
    }
}

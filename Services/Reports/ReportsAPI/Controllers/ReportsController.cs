using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportsAPI.Data;
using ReportsAPI.Repositories;
using System;
using System.Threading.Tasks;

namespace ReportsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IDataRepository db;

        public ReportsController(IDataRepository db)
        {
            this.db = db;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await db.GetReportObject(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = db.GetAllReportObjects();
            return Ok(result);
        }
    }
}

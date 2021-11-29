using ContactsAPI.Application.Communications;
using ContactsAPI.Application.ContactsInfo.Commands;
using ContactsAPI.Application.ContactsInfo.Queries;
using ContactsAPI.Application.Reports.Commands;
using ContactsAPI.Application.ReportsInfo.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Domains;
using System;
using System.Threading.Tasks;

namespace ContactsAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ContactsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {

            var result = await mediator.Send(new GetContactByIdQuery { ContactId = id });
            return Ok(result);
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll([FromBody] GetAllContactsQuery req)
        {
            var result = await mediator.Send(req);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateContactCommand command)
        {
            Guid result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateContactCommand command)
        {
            bool result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            bool result = await mediator.Send(new DeleteContactCommand { KisiId = id });
            return Ok(result);
        }

        [HttpGet("PrepareReport")]
        public async Task<IActionResult> PrepareReport()
        {
            var result = await mediator.Send(new PrepareReportCommand());
            result.Reports = null;
            return Ok(result);
        }

        [HttpGet("GetAllReports")]
        public async Task<IActionResult> GetPreparedReports()
        {
            var result = await mediator.Send(new GetAllReportQuery());
            return Ok(result);
        }
    }
}

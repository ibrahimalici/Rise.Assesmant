using ContactsAPI.Application.Kisiler.Commands;
using ContactsAPI.Application.Kisiler.Queries;
using ContactsAPI.Application.Reports.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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

            var result = await mediator.Send(new GetKisiByIdQuery { KisiId = id });
            return Ok(result);
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll([FromBody] GetAllKisilerQuery req)
        {
            var result = await mediator.Send(req);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateKisiCommand command)
        {
            Guid result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateKisiCommand command)
        {
            bool result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool result = await mediator.Send(new DeleteKisiCommand { KisiId = id });
            return Ok(result);
        }

        [HttpGet("PrepareReport")]
        public async Task<IActionResult> PrepareReport()
        {
            var result = await mediator.Send(new PrepareReportCommand());
            return Ok(result);
        }
    }
}

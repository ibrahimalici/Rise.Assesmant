using ContactsAPI.Application.ContactSubDetails.Commands;
using ContactsAPI.Application.ContactSubDetails.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Domains;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactsAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ContactDetailsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ContactDetailsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {

            var result = await mediator.Send(new GetContactDetailByIdQuery { ContactDetailId = id });
            return Ok(result);
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll([FromBody] GetAllContactDetailsQuery req)
        {
            var result = await mediator.Send(req);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateContactDetailCommand command)
        {
            Guid result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UpdateContactDetailCommand command)
        {
            bool result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            bool result = await mediator.Send(new DeleteContactDetailCommand { ContactDetailId = id });
            return Ok(result);
        }
    }
}

using ContactsAPI.Application.IletisimBilgileri.Commands;
using ContactsAPI.Application.IletisimBilgileri.Queries;
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

            var result = await mediator.Send(new GetIletisimByIdQuery { IletisimId = id });
            return Ok(result);
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll([FromBody] GetAllIletisimQuery req)
        {
            var result = await mediator.Send(req);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateIletisimCommand command)
        {
            Guid result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateIletisimCommand command)
        {
            bool result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool result = await mediator.Send(new DeleteIletisimCommand { IletisimId = id });
            return Ok(result);
        }
    }
}

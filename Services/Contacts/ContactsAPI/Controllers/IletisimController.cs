using ContactsAPI.Application.IletisimBilgileri.Commands;
using ContactsAPI.Application.IletisimBilgileri.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Messages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactsAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class IletisimController : ControllerBase
    {
        private readonly IMediator mediator;

        public IletisimController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IletisimDTO> Get([FromRoute] Guid id)
        {

            var result = await mediator.Send(new GetIletisimByIdQuery { IletisimId = id });
            return result;
        }

        [HttpPost("GetAllIletisim")]
        public async Task<List<IletisimDTO>> GetAllIletisim([FromBody] GetAllIletisimQuery req)
        {
            var result = await mediator.Send(req);
            return result;
        }

        [HttpPost]
        public async Task<Guid> Post(CreateIletisimCommand command)
        {
            Guid result = await mediator.Send(command);
            return result;
        }

        [HttpPut]
        public async Task<bool> Put(UpdateIletisimCommand command)
        {
            bool result = await mediator.Send(command);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(Guid id)
        {
            bool result = await mediator.Send(new DeleteIletisimCommand { IletisimId = id });
            return result;
        }
    }
}

using ContactsAPI.Application.Kisiler.Commands;
using ContactsAPI.Application.Kisiler.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Domains;
using SharedLibrary.Messages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactsAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class KisilerController : ControllerBase
    {
        private readonly IMediator mediator;

        public KisilerController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<KisiDTO> Get([FromRoute] Guid id)
        {

            var result = await mediator.Send(new GetKisiByIdQuery { KisiId = id });
            return result;
        }

        [HttpPost("GetAllKisiler")]
        public async Task<List<KisiDTO>> GetAllKisiler([FromBody]GetAllKisilerQuery req)
        {
            var result = await mediator.Send(req);
            return result;
        }

        [HttpPost]
        public async Task<Guid> Post(CreateKisiCommand command)
        {
            Guid result = await mediator.Send(command);
            return result;
        }

        [HttpPut]
        public async Task<bool> Put(UpdateKisiCommand command)
        {
            bool result = await mediator.Send(command);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(Guid id)
        {
            bool result = await mediator.Send(new DeleteKisiCommand { KisiId = id });
            return result;
        }
    }
}

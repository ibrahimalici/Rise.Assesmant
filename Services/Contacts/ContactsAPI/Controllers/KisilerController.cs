using ContactsAPI.Application.Kisiler.Commands;
using ContactsAPI.Application.Kisiler.Queries;
using ContactsAPI.Domains;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactsAPI.Controllers
{
    [Route("api/[controller]")]
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

            var result = await mediator.Send(new GetIletisimByIdQuery { KisiId = id });
            return result;
        }

        [HttpPost("GetAllKisiler")]
        public async Task<List<KisiDTO>> GetAllKisiler([FromBody]GetAllIletisimQuery req)
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
            bool result = await mediator.Send(new DeleteIletisimCommand { KisiId = id });
            return result;
        }
    }
}

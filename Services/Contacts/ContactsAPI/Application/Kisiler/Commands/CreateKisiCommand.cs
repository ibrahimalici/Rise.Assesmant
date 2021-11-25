using AutoMapper;
using ContactsAPI.Application.Communications;
using ContactsAPI.Entities;
using ContactsAPI.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Messages;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ContactsAPI.Application.Kisiler.Commands
{
    public class CreateKisiCommand : IRequest<Guid>
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Firma { get; set; }
    }

    public class CreateKisiHandle : IRequestHandler<CreateKisiCommand, Guid>
    {
        private readonly DatabaseContext db;
        private readonly MassTransitHelper queueHelper;
        private readonly IMapper mapper;

        public CreateKisiHandle(DatabaseContext db, MassTransitHelper queueHelper, IMapper mapper)
        {
            this.db = db;
            this.queueHelper = queueHelper;
            this.mapper = mapper;
        }

        public async Task<Guid> Handle(CreateKisiCommand request, CancellationToken cancellationToken)
        {
            Kisi data = new Kisi
            {
                Ad = request.Ad,
                Soyad = request.Soyad,
                Firma = request.Firma,
                Id = Guid.NewGuid()
            };
            await db.Kisiler.AddAsync(data);
            await db.SaveChangesAsync();

            Kisi sendData = await db.Kisiler.Include(o => o.IletisimBilgileri).FirstOrDefaultAsync(x => x.Id == data.Id);
            KisiDTO message = mapper.Map<KisiDTO>(sendData);
            queueHelper.SendKisi(message).Start();
            return data.Id;
        }
    }
}

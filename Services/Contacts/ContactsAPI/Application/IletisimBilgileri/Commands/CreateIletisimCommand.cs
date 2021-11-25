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

namespace ContactsAPI.Application.IletisimBilgileri.Commands
{
    public class CreateIletisimCommand : IRequest<Guid>
    {
        public ContactDetailType BilgiTipi { get; set; }
        public string BilgiIcerigi { get; set; }
        public Guid KisiId { get; set; }
    }

    public class CreateIletisimHandle : IRequestHandler<CreateIletisimCommand, Guid>
    {
        private readonly DatabaseContext db;
        private readonly MassTransitHelper queueHelper;
        private readonly IMapper mapper;

        public CreateIletisimHandle(DatabaseContext db, MassTransitHelper queueHelper, IMapper mapper)
        {
            this.db = db;
            this.queueHelper = queueHelper;
            this.mapper = mapper;
        }

        public async Task<Guid> Handle(CreateIletisimCommand request, CancellationToken cancellationToken)
        {
            Iletisim data = new Iletisim
            {
                Id= Guid.NewGuid(),
                KisiId = request.KisiId,
                BilgiTipi = request.BilgiTipi,
                BilgiIcerigi = request.BilgiIcerigi
            };
            await db.IletisimBilgileri.AddAsync(data);
            await db.SaveChangesAsync();

            Kisi sendData = await db.Kisiler.Include(o => o.IletisimBilgileri).FirstOrDefaultAsync(x => x.Id == data.KisiId);
            KisiDTO message = mapper.Map<KisiDTO>(sendData);
            queueHelper.SendKisi(message).Start();

            return data.Id;
        }
    }
}

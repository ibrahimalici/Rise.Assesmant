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
    public class UpdateKisiCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Firma { get; set; }
    }

    public class UpdateKisiHandle : IRequestHandler<UpdateKisiCommand, bool>
    {
        private readonly DatabaseContext db;
        private readonly MassTransitHelper queueHelper;
        private readonly IMapper mapper;

        public UpdateKisiHandle(DatabaseContext db, MassTransitHelper queueHelper, IMapper mapper)
        {
            this.db = db;
            this.queueHelper = queueHelper;
            this.mapper = mapper;
        }

        public async Task<bool> Handle(UpdateKisiCommand request, CancellationToken cancellationToken)
        {
            Kisi saved = await db.Kisiler.FindAsync(request.Id);

            saved.Ad = request.Ad;
            saved.Firma =request.Firma;
            saved.Soyad =request.Soyad;
            await db.SaveChangesAsync();

            Kisi sendData = await db.Kisiler.Include(o => o.IletisimBilgileri).FirstOrDefaultAsync(x => x.Id == saved.Id);
            KisiDTO message = mapper.Map<KisiDTO>(sendData);
            queueHelper.SendKisi(message).Start();

            return true;
        }
    }
}

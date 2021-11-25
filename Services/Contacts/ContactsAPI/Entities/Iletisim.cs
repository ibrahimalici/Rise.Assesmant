using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedLibrary.Messages;
using System;

namespace ContactsAPI.Entities
{
    public class Iletisim
    {
        public Guid Id { get; set; }
        public ContactDetailType BilgiTipi { get; set; }
        public string BilgiIcerigi { get; set; }
        public Guid KisiId { get; set; }
        public Kisi Kisi { get; set; }
    }

    public class IletisimConfiguration : IEntityTypeConfiguration<Iletisim>
    {
        public void Configure(EntityTypeBuilder<Iletisim> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.BilgiIcerigi).HasMaxLength(120).IsRequired();
        }
    }
}

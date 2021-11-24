using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContactsAPI.Entities
{
    public class Kisi
    {
        [Key]
        public Guid Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Firma { get; set; }

        public List<Iletisim> IletisimBilgileri = new List<Iletisim>();
    }

    public class KisiConfiguration : IEntityTypeConfiguration<Kisi>
    {
        public void Configure(EntityTypeBuilder<Kisi> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Ad).HasMaxLength(80).IsRequired();
            builder.Property(x => x.Soyad).HasMaxLength(80).IsRequired();
            builder.Property(x => x.Firma).HasMaxLength(80);
        }
    }
}

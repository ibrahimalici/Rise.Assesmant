using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContactsAPI.Entities
{
    public class Contact
    {
        [Key]
        public Guid ContactId { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Firma { get; set; }

        public List<ContactDetail> ContactSubDetails = new List<ContactDetail>();
    }

    public class KisiConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(x => x.ContactId);
            builder.Property(x=>x.Ad).HasMaxLength(80).IsRequired();
            builder.Property(x => x.Soyad).HasMaxLength(80).IsRequired();
            builder.Property(x => x.Firma).HasMaxLength(80);
        }
    }
}

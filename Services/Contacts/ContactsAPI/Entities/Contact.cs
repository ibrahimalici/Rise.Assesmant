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
        public string Name { get; set; }
        public string Surename { get; set; }
        public string Company { get; set; }

        public List<ContactDetail> ContactSubDetails = new List<ContactDetail>();
    }

    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(x => x.ContactId);
            builder.Property(x=>x.Name).HasMaxLength(80).IsRequired();
            builder.Property(x => x.Surename).HasMaxLength(80).IsRequired();
            builder.Property(x => x.Company).HasMaxLength(80);
        }
    }
}

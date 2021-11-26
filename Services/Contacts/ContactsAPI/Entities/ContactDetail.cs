using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedLibrary.Domains;
using SharedLibrary.Messages;
using System;

namespace ContactsAPI.Entities
{
    public class ContactDetail
    {
        public Guid Id { get; set; }
        public ContactDetailType BilgiTipi { get; set; }
        public string BilgiIcerigi { get; set; }
        public Guid KisiId { get; set; }
        public Contact Kisi { get; set; }
    }

    public class IletisimConfiguration : IEntityTypeConfiguration<ContactDetail>
    {
        public void Configure(EntityTypeBuilder<ContactDetail> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.BilgiIcerigi).HasMaxLength(120).IsRequired();
        }
    }
}

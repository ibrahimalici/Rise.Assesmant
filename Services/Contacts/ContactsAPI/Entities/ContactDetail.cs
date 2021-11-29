using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedLibrary.Domains;
using SharedLibrary.Messages;
using System;

namespace ContactsAPI.Entities
{
    public class ContactDetail
    {
        public Guid ContactDetailId { get; set; }
        public ContactDetailType ContactDetailType { get; set; }
        public string Description { get; set; }
        public Guid ContactId { get; set; }
        public Contact Contact { get; set; }
    }

    public class IletisimConfiguration : IEntityTypeConfiguration<ContactDetail>
    {
        public void Configure(EntityTypeBuilder<ContactDetail> builder)
        {
            builder.HasKey(x => x.ContactDetailId);
            builder.Property(x=>x.Description).HasMaxLength(120).IsRequired();
        }
    }
}

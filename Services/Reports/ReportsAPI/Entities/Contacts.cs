using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReportsAPI.Entities
{
    public class Contact : BaseEntity
    {
        public Guid ContactId { get; set; }
        public string Name { get; set; }
        public string Surename { get; set; }
        public string Company { get; set; }

        public List<ContactDetail> ContactSubDetails = new List<ContactDetail>();
    }
}

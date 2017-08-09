using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyMassage.Backend.Models
{
    public class Service : BusinessEntityBase
    {
        [Required]
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("textList")]
        public List<string> TextList = new List<string>();

        [Required]
        [BsonElement("items")]
        public List<ServiceItem> Items = new List<ServiceItem>();
    }
}

using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyMassage.Backend.Models
{
    public class ServiceItem
    {
        [Required]
        [BsonElement("minute")]
        public int Minute { get; set; }

        [Required]
        [BsonElement("price")]
        public int Price { get; set; }
    }
}

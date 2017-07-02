using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyMassage.Backend.Models
{
    public class Service
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public List<string> TextList = new List<string>();

        [Required]
        public List<ServiceItem> Items = new List<ServiceItem>();
    }
}

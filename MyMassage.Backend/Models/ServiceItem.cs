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
        public int Minute { get; set; }

        [Required]
        public int Price { get; set; }
    }
}

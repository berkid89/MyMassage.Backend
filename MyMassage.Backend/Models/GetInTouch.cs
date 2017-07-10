using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMassage.Backend.Models
{
    public class GetInTouch : BusinessEntityBase
    {
        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
    }
}

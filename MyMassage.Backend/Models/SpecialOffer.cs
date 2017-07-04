using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMassage.Backend.Models
{
    public class SpecialOffer
    {
        public bool HasSpecialOffer { get; set; }

        public int Percent { get; set; }

        public string Text { get; set; }
    }
}

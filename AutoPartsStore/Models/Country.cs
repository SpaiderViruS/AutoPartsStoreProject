using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AutoPartsStore.Models
{
    public partial class Country
    {
        public Country()
        {
            Manufracturer = new HashSet<Manufracturer>();
        }

        public int IdCountry { get; set; }
        public string CountryName { get; set; }

        public virtual ICollection<Manufracturer> Manufracturer { get; set; }
    }
}

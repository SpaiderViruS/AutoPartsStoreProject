using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AutoPartsStore.Models
{
    public partial class Manufracturer
    {
        public Manufracturer()
        {
            Autopart = new HashSet<Autopart>();
        }

        public int IdManufracturer { get; set; }
        public string ManufracturerName { get; set; }
        public int IdCountry { get; set; }

        public virtual Country IdCountryNavigation { get; set; }
        public virtual ICollection<Autopart> Autopart { get; set; }
    }
}

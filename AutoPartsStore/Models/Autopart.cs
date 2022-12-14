using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AutoPartsStore.Models
{
    public partial class Autopart
    {
        public Autopart()
        {
            Busketautopart = new HashSet<Busketautopart>();
            Review = new HashSet<Review>();
        }

        public int IdAutoPart { get; set; }
        public string AutoPartName { get; set; }
        public int Cost { get; set; }
        public string Description { get; set; }
        public string AutoPartImage { get; set; }
        public int IdManufracturer { get; set; }
        public int IdCharacteristik { get; set; }
        public int IdStatusAutoPart { get; set; }

        public virtual Characteristik IdCharacteristikNavigation { get; set; }
        public virtual Manufracturer IdManufracturerNavigation { get; set; }
        public virtual Status IdStatusAutoPartNavigation { get; set; }
        public virtual ICollection<Busketautopart> Busketautopart { get; set; }
        public virtual ICollection<Review> Review { get; set; }
    }
}

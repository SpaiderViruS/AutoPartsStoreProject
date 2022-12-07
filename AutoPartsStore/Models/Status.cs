using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AutoPartsStore.Models
{
    public partial class Status
    {
        public Status()
        {
            Autopart = new HashSet<Autopart>();
        }

        public int IdStatus { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<Autopart> Autopart { get; set; }
    }
}

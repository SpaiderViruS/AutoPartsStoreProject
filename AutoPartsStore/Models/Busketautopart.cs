using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AutoPartsStore.Models
{
    public partial class Busketautopart
    {
        public int IdBusketAutopart { get; set; }
        public int IdBusket { get; set; }
        public int IdAutopart { get; set; }

        public virtual Autopart IdAutopartNavigation { get; set; }
        public virtual Busket IdBusketNavigation { get; set; }
    }
}

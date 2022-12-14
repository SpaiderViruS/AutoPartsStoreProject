using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AutoPartsStore.Models
{
    public partial class Busket
    {
        public Busket()
        {
            Busketautopart = new HashSet<Busketautopart>();
            Order = new HashSet<Order>();
        }

        public int IdBusket { get; set; }
        public int IdUser { get; set; }
        public string OrderStatus { get; set; }

        public virtual User IdUserNavigation { get; set; }
        public virtual ICollection<Busketautopart> Busketautopart { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}

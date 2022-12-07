using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AutoPartsStore.Models
{
    public partial class User
    {
        public User()
        {
            Busket = new HashSet<Busket>();
            Review = new HashSet<Review>();
        }

        public int IdUser { get; set; }
        public string Surname { get; set; }
        public string Patronomyc { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public int IdRole { get; set; }

        public virtual Role IdRoleNavigation { get; set; }
        public virtual ICollection<Busket> Busket { get; set; }
        public virtual ICollection<Review> Review { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace TicketGenerator.Models
{
    public partial class Login
    {
        public Login()
        {
            MultipleComments = new HashSet<MultipleComment>();
            Tickects = new HashSet<Tickect>();
            UserRoles = new HashSet<UserRole>();
        }

        public int Uid { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailId { get; set; }
        public string? PhoneNo { get; set; }
        public string? UserName { get; set; }
        public string? PassWord { get; set; }
        public DateTime? SignIn { get; set; }
        public DateTime? SignOut { get; set; }
        public int? BlockCount { get; set; }

        public virtual ICollection<MultipleComment> MultipleComments { get; set; }
        public virtual ICollection<Tickect> Tickects { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}

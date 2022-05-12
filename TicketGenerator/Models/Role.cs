using System;
using System.Collections.Generic;

namespace TicketGenerator.Models
{
    public partial class Role
    {
        public Role()
        {
            Logins = new HashSet<Login>();
            UserRoles = new HashSet<UserRole>();
        }

        public int RoleId { get; set; }
        public string? RoleName { get; set; }

        public virtual ICollection<Login> Logins { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}

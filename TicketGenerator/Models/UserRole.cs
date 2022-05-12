using System;
using System.Collections.Generic;

namespace TicketGenerator.Models
{
    public partial class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int? Uid { get; set; }

        public virtual Role? Role { get; set; }
        public virtual Login? UidNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace TicketGenerator.Models
{
    public partial class MultipleComment
    {
        public int Id { get; set; }
        public int? Uid { get; set; }
        public string? Comments { get; set; }

        public virtual Login? UidNavigation { get; set; }
    }
}

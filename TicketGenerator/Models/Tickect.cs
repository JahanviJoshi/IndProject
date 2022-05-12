using System;
using System.Collections.Generic;

namespace TicketGenerator.Models
{
    public partial class Tickect
    {
        public int TicketNo { get; set; }
        public int? Uid { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public DateTime? DateAndTime { get; set; }
        public int? Status { get; set; }
        public string? AssignTo { get; set; }

        public virtual Login? UidNavigation { get; set; }
    }
}

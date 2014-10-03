using System;
using System.Collections.Generic;

namespace webapi.atom.Models
{
    /// <summary>
    /// Event indicating that a ticket has been created.
    /// </summary>
    public class TicketCreatedEvent : Event
    {
        public Guid TicketId { get; set; }

        public Guid AssignedUserId { get; set; }

        public Guid CreatorUserId { get; set; }

        public Dictionary<string, object> AdditionalValues { get; set; } 
    }
}
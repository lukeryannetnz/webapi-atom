using System;

namespace webapi.atom.Models
{
    /// <summary>
    /// Event indicating that a ticket has been modified.
    /// </summary>
    public class TicketModifiedEvent : Event
    {
        public Guid TicketId { get; set; }

        public Guid ModifiedByUserId { get; set; }
    }
}
using System;
using System.Runtime.Serialization;

namespace webapi.atom.Models
{
    // KnownType necessary for datacontract xml serializer. Poo.
    [KnownType(typeof(TicketCreatedEvent))]
    [KnownType(typeof(TicketModifiedEvent))]
    public abstract class Event
    {
        public DateTime TimestampUtc { get; set; }
    }
}
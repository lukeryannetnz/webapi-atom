using System;
using System.Collections.Generic;
using System.Web.Http;
using webapi.atom.Models;

namespace webapi.atom.Controllers
{
    public class TicketEventsController : ApiController
    {
        public IEnumerable<Event> Get()
        {
            return GetMockData();
        }

        private IEnumerable<Event> GetMockData()
        {
            var inviteTrue = new Dictionary<string, object>() {{"Invite", true}};

            return new List<Event>()
            {
                new TicketCreatedEvent()
                {
                    AdditionalValues = inviteTrue,
                    AssignedUserId = Guid.NewGuid(),
                    CreatorUserId = Guid.NewGuid(),
                    TicketId = Guid.NewGuid(),
                    TimestampUtc = DateTime.UtcNow
                },
                new TicketCreatedEvent()
                {
                    AdditionalValues = inviteTrue,
                    AssignedUserId = Guid.NewGuid(),
                    CreatorUserId = Guid.NewGuid(),
                    TicketId = Guid.NewGuid(),
                    TimestampUtc = DateTime.UtcNow
                },
                new TicketModifiedEvent()
                {
                    ModifiedByUserId = Guid.NewGuid(),
                    TicketId = Guid.NewGuid(),
                    TimestampUtc = DateTime.UtcNow
                },
                new TicketCreatedEvent()
                {
                    AdditionalValues = inviteTrue,
                    AssignedUserId = Guid.NewGuid(),
                    CreatorUserId = Guid.NewGuid(),
                    TicketId = Guid.NewGuid(),
                    TimestampUtc = DateTime.UtcNow
                }
            };
        }
    }
}
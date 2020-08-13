using System;

namespace TicketFlow.ProfileService.Client.Extensibility.Entities
{
    public interface IProfile
    {
        public int Id { get; }

        public string UserEmail { get; }

        public long PhoneNumber { get; }

        public DateTime Birthday { get; }
    }
}
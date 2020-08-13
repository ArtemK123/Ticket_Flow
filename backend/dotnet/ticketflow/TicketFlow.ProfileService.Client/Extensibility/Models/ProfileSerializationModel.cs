using System;

namespace TicketFlow.ProfileService.Client.Extensibility.Models
{
    public class ProfileSerializationModel
    {
        public int Id { get; set; }

        public string UserEmail { get; set; }

        public long PhoneNumber { get; set; }

        public DateTime Birthday { get; set; }
    }
}
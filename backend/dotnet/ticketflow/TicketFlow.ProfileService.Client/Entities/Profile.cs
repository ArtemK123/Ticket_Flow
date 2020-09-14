using System;
using TicketFlow.ProfileService.Client.Extensibility.Entities;

namespace TicketFlow.ProfileService.Client.Entities
{
    internal class Profile : IProfile
    {
        public Profile(int id, string userEmail, long phoneNumber, DateTime birthday)
        {
            Id = id;
            UserEmail = userEmail;
            PhoneNumber = phoneNumber;
            Birthday = birthday;
        }

        public int Id { get; }

        public string UserEmail { get; }

        public long PhoneNumber { get; }

        public DateTime Birthday { get; }
    }
}
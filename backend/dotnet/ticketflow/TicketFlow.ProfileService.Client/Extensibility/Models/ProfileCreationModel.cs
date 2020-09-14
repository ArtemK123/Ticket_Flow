using System;

namespace TicketFlow.ProfileService.Client.Extensibility.Models
{
    public class ProfileCreationModel
    {
        public ProfileCreationModel(string userEmail, long phoneNumber, DateTime birthday)
        {
            UserEmail = userEmail;
            PhoneNumber = phoneNumber;
            Birthday = birthday;
        }

        public string UserEmail { get; }

        public long PhoneNumber { get; }

        public DateTime Birthday { get; }
    }
}
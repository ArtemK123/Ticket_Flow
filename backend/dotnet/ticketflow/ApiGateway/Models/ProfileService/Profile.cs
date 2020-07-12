using System;

namespace ApiGateway.Models.ProfileService
{
    public class Profile
    {
        public Profile(int id, string userEmail, long phoneNumber, DateTime birthday)
        {
            Id = id;
            UserEmail = userEmail;
            PhoneNumber = phoneNumber;
            Birthday = birthday;
        }

        public Profile(string userEmail, long phoneNumber, DateTime birthday)
        {
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
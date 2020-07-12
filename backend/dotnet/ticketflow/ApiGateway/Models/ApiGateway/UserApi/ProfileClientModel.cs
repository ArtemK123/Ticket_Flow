using System;

namespace ApiGateway.Models.ApiGateway.UserApi
{
    public class ProfileClientModel
    {
        public ProfileClientModel(long phoneNumber, DateTime birthday)
        {
            PhoneNumber = phoneNumber;
            Birthday = birthday;
        }

        public long PhoneNumber { get; }

        public DateTime Birthday { get; }
    }
}
using System;

namespace ProfileService.Models
{
    public class Profile
    {
        public int Id { get; set; }

        public string UserEmail { get; set; }

        public long PhoneNumber { get; set; }

        public DateTime Birthday { get; set; }
    }
}
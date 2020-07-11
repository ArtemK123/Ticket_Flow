using System;
using System.ComponentModel.DataAnnotations;

namespace ProfileService.Api.ClientModels
{
    public class ProfileClientModel
    {
        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }

        [Required]
        public long PhoneNumber { get; set; }

        [Required]
        public DateTime Birthday { get; set; }
    }
}
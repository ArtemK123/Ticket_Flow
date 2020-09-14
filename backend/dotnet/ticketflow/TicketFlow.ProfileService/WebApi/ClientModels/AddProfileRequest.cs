using System;
using System.ComponentModel.DataAnnotations;

namespace TicketFlow.ProfileService.WebApi.ClientModels
{
    public class AddProfileRequest
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
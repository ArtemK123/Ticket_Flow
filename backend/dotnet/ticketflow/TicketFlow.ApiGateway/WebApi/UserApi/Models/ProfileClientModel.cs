using System;

namespace TicketFlow.ApiGateway.WebApi.UserApi.Models
{
    public class ProfileClientModel
    {
        public long PhoneNumber { get; set; }

        public DateTime Birthday { get; set; }
    }
}
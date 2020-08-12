using System;

namespace TicketFlow.ProfileService.Domain.Models
{
    public class StoredProfileCreationModel : ProfileCreationModel
    {
        public StoredProfileCreationModel(int id, string userEmail, long phoneNumber, DateTime birthday)
            : base(userEmail, phoneNumber, birthday)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
using System;

namespace TicketFlow.ProfileService.Client.Extensibility.Models
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
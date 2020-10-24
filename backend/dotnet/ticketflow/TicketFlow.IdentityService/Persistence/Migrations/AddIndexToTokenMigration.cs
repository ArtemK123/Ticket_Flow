using FluentMigrator;

namespace TicketFlow.IdentityService.Persistence.Migrations
{
    [Migration(2)]
    public class AddIndexToTokenMigration : Migration
    {
        public override void Up()
        {
            Create.Index("ix_token")
                .OnTable("users")
                .OnColumn("token")
                .Ascending()
                .WithOptions()
                .NonClustered();
        }

        public override void Down()
        {
            Delete.Index("ix_token");
        }
    }
}
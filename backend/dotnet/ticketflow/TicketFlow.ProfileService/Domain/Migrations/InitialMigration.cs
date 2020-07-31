using FluentMigrator;

namespace TicketFlow.ProfileService.Domain.Migrations
{
    [Migration(1)]
    public class InitialMigration : Migration
    {
        public override void Up()
        {
            Create.Table("profiles")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("birthday").AsDate()
                .WithColumn("phone_number").AsInt64()
                .WithColumn("user_email").AsString(255);
        }

        public override void Down()
        {
            Delete.Table("profiles");
        }
    }
}
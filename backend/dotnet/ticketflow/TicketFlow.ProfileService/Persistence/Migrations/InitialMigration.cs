using FluentMigrator;

namespace TicketFlow.ProfileService.Persistence.Migrations
{
    [Migration(1)]
    public class InitialMigration : Migration
    {
        public override void Up()
        {
            Create.Table("profiles")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("birthday").AsDate().NotNullable()
                .WithColumn("phone_number").AsInt64().NotNullable()
                .WithColumn("user_email").AsString(255).NotNullable().Unique();
        }

        public override void Down()
        {
            Delete.Table("profiles");
        }
    }
}
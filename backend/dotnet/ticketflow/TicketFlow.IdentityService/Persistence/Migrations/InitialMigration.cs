using FluentMigrator;

namespace TicketFlow.IdentityService.Persistence.Migrations
{
    [Migration(1)]
    public class InitialMigration : Migration
    {
        public override void Up()
        {
            Create.Table("users")
                .WithColumn("email").AsString(255).PrimaryKey()
                .WithColumn("password").AsString(255).NotNullable()
                .WithColumn("role").AsInt32()
                .WithColumn("token").AsString(255).Nullable();
        }

        public override void Down()
        {
            Delete.Table("users");
        }
    }
}
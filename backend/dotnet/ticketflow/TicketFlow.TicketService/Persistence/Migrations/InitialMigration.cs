using FluentMigrator;

namespace TicketFlow.TicketService.Persistence.Migrations
{
    [Migration(1)]
    public class InitialMigration : Migration
    {
        public override void Up()
        {
            Create.Table("tickets")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("buyer_email").AsString(255).Nullable()
                .WithColumn("movie_id").AsInt32().NotNullable()
                .WithColumn("row").AsInt32().NotNullable()
                .WithColumn("seat").AsInt32().NotNullable()
                .WithColumn("price").AsInt32().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("tickets");
        }
    }
}
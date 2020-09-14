using FluentMigrator;

namespace TicketFlow.MovieService.Persistence.Migrations
{
    [Migration(1)]
    public class InitialMigration : Migration
    {
        public override void Up()
        {
            Create.Table("cinema_halls")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("name").AsString(255).NotNullable()
                .WithColumn("location").AsString(255).NotNullable()
                .WithColumn("seat_rows").AsInt32().NotNullable()
                .WithColumn("seats_in_row").AsInt32().NotNullable();

            Create.Table("films")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("title").AsString(255).NotNullable()
                .WithColumn("description").AsString(255).NotNullable()
                .WithColumn("premiere_date").AsDate().NotNullable()
                .WithColumn("creator").AsString(255).NotNullable()
                .WithColumn("age_limit").AsInt32().NotNullable()
                .WithColumn("duration").AsInt32().NotNullable();

            Create.Table("movies")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("start_time").AsDateTime().NotNullable()
                .WithColumn("cinema_hall_id").AsInt32().ForeignKey("cinema_halls", "id")
                .WithColumn("film_id").AsInt32().ForeignKey("films", "id");
        }

        public override void Down()
        {
            Delete.Table("movies");
            Delete.Table("cinema_halls");
            Delete.Table("films");
        }
    }
}
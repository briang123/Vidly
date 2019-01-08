namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Genres : DbMigration
    {
        public override void Up()
        {
            Sql("insert into Genres (Id, Name) values (1, 'Action')");
            Sql("insert into Genres (Id, Name) values (2, 'Thriller')");
            Sql("insert into Genres (Id, Name) values (3, 'Family')");
            Sql("insert into Genres (Id, Name) values (4, 'Romance')");
            Sql("insert into Genres (Id, Name) values (5, 'Comedy')");
        }

        public override void Down()
        {
        }
    }
}

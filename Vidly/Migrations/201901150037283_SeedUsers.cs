namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'73fc61d3-cea7-4f0e-a29e-57d7e33da315', N'admin@vidly.com', 0, N'AE5IFYrrmw69g3dB+gGrGoTsQmeHLh321W0r/dJIzIsRpOwaaU2shzewb52T4IrnsA==', N'ebf4bcc2-afc0-4568-a2a2-868f4106fb69', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'c0f05504-0d6a-4153-a7cf-4f1da9bdfcef', N'guest@vidly.com', 0, N'AGHiYG5ynzsuNL+LkEnVDMuj+lroNwJN2Rd8e0nYBZiM2vl8DulhvonXZ9fAFAtpag==', N'acc49f4d-3094-4260-bbe2-26e6810103fc', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')

                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'b1a47339-bb3b-4fee-9a1e-74af9d3b7f2f', N'CanManageMovies')

                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'73fc61d3-cea7-4f0e-a29e-57d7e33da315', N'b1a47339-bb3b-4fee-9a1e-74af9d3b7f2f')
            ");
        }

        public override void Down()
        {
        }
    }
}

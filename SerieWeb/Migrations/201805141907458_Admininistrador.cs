namespace SerieWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Admininistrador : DbMigration
    {
        public override void Up()
        {
            Sql(@"

       INSERT INTO[dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], 
       [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], 
       [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES(N'9279ad8e-d0d2-4097-b35f-3b5860e00ee0', 
       N'engeziellithonvieira@gmail.com', 1, N'APeLpClHjlU3Rwyhv91i2Z86XCBPu0nrvFtWjrVKYmaP7m8v3Dab55KZTsX3ifvFEw==', 
       N'deeb9ecf-be4b-432a-bfc3-0f5de03a0547', NULL, 0, 0, NULL, 1, 0, N'engeziellithonvieira@gmail.com')
       
       INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'e20fd023-f343-4778-a6f5-97b5a1f493f5', N'SuperAdmin')


       INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'9279ad8e-d0d2-4097-b35f-3b5860e00ee0', N'e20fd023-f343-4778-a6f5-97b5a1f493f5')

       

       ");
        }
        
        public override void Down()
        {
        }
    }
}

namespace SerieWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class trailer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Serie", "Trailer", c => c.String());
            DropColumn("dbo.Serie", "ImagemBanner");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Serie", "ImagemBanner", c => c.String());
            DropColumn("dbo.Serie", "Trailer");
        }
    }
}

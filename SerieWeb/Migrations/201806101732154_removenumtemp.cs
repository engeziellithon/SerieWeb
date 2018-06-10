namespace SerieWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removenumtemp : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Temporada", "NumeroTemporada");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Temporada", "NumeroTemporada", c => c.Int(nullable: false));
        }
    }
}

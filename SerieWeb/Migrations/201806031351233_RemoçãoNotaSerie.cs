namespace SerieWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoçãoNotaSerie : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Serie", "Nota");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Serie", "Nota", c => c.Double(nullable: false));
        }
    }
}

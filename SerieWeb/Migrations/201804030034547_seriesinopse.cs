namespace SerieWeb.Migrations
{
    
    using System.Data.Entity.Migrations;
    
    public partial class seriesinopse : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Serie", "Sinopse", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Serie", "Sinopse", c => c.String(nullable: false, maxLength: 1000));
        }
    }
}

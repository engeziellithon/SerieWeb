namespace SerieWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PreçoServiço : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServicoStreaming", "Preco", c => c.Double(nullable: false));
            DropColumn("dbo.Genero", "Preco");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Genero", "Preco", c => c.Double(nullable: false));
            DropColumn("dbo.ServicoStreaming", "Preco");
        }
    }
}

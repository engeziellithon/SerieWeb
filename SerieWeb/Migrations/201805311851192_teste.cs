namespace SerieWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teste : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ServicoStreaming", "Preco", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ServicoStreaming", "Preco", c => c.Double(nullable: false));
        }
    }
}

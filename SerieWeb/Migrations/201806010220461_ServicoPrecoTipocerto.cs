namespace SerieWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ServicoPrecoTipocerto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServicoStreaming", "Preco", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServicoStreaming", "Preco");
        }
    }
}

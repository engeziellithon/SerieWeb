namespace SerieWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GeneroSerieServico : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SerieGeneroes", "GeneroID", "dbo.Genero");
            DropForeignKey("dbo.SerieGeneroes", "SerieID", "dbo.Serie");
            DropForeignKey("dbo.SerieServicoes", "SerieID", "dbo.Serie");
            DropForeignKey("dbo.SerieServicoes", "Servico_ServicoStreamingID", "dbo.ServicoStreaming");
            DropIndex("dbo.SerieGeneroes", new[] { "SerieID" });
            DropIndex("dbo.SerieGeneroes", new[] { "GeneroID" });
            DropIndex("dbo.SerieServicoes", new[] { "SerieID" });
            DropIndex("dbo.SerieServicoes", new[] { "Servico_ServicoStreamingID" });
            DropTable("dbo.SerieGeneroes");
            DropTable("dbo.SerieServicoes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SerieServicoes",
                c => new
                    {
                        SerieServicoID = c.Int(nullable: false, identity: true),
                        SerieID = c.Int(nullable: false),
                        ServicoID = c.Int(nullable: false),
                        Servico_ServicoStreamingID = c.Int(),
                    })
                .PrimaryKey(t => t.SerieServicoID);
            
            CreateTable(
                "dbo.SerieGeneroes",
                c => new
                    {
                        SerieGeneroID = c.Int(nullable: false, identity: true),
                        SerieID = c.Int(nullable: false),
                        GeneroID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SerieGeneroID);
            
            CreateIndex("dbo.SerieServicoes", "Servico_ServicoStreamingID");
            CreateIndex("dbo.SerieServicoes", "SerieID");
            CreateIndex("dbo.SerieGeneroes", "GeneroID");
            CreateIndex("dbo.SerieGeneroes", "SerieID");
            AddForeignKey("dbo.SerieServicoes", "Servico_ServicoStreamingID", "dbo.ServicoStreaming", "ServicoStreamingID");
            AddForeignKey("dbo.SerieServicoes", "SerieID", "dbo.Serie", "SerieID", cascadeDelete: true);
            AddForeignKey("dbo.SerieGeneroes", "SerieID", "dbo.Serie", "SerieID", cascadeDelete: true);
            AddForeignKey("dbo.SerieGeneroes", "GeneroID", "dbo.Genero", "GeneroID", cascadeDelete: true);
        }
    }
}

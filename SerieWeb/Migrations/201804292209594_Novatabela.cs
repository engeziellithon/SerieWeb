namespace SerieWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Novatabela : DbMigration
    {
        public override void Up()
        {
            
            CreateTable(
                "dbo.SeriesGeneros",
                c => new
                    {
                        SerieGenerosID = c.Int(nullable: false, identity: true),
                        SerieID = c.Int(nullable: false),
                        GeneroID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SerieGenerosID)
                .ForeignKey("dbo.Genero", t => t.GeneroID, cascadeDelete: true)
                .ForeignKey("dbo.Serie", t => t.SerieID, cascadeDelete: true)
                .Index(t => t.SerieID)
                .Index(t => t.GeneroID);
            
            CreateTable(
                "dbo.SeriesServicos",
                c => new
                    {
                        SerieServicosID = c.Int(nullable: false, identity: true),
                        SerieID = c.Int(nullable: false),
                        ServicoStreamingID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SerieServicosID)
                .ForeignKey("dbo.Serie", t => t.SerieID, cascadeDelete: true)
                .ForeignKey("dbo.ServicoStreaming", t => t.ServicoStreamingID, cascadeDelete: true)
                .Index(t => t.SerieID)
                .Index(t => t.ServicoStreamingID);
            
           
        }
        
        public override void Down()
        {
            AddColumn("dbo.Serie", "ServicoStreaming_ServicoStreamingID", c => c.Int());
            AddColumn("dbo.Serie", "Genero_GeneroID", c => c.Int());
            DropForeignKey("dbo.SeriesServicos", "ServicoStreamingID", "dbo.ServicoStreaming");
            DropForeignKey("dbo.SeriesServicos", "SerieID", "dbo.Serie");
            DropForeignKey("dbo.SeriesGeneros", "SerieID", "dbo.Serie");
            DropForeignKey("dbo.SeriesGeneros", "GeneroID", "dbo.Genero");
            DropIndex("dbo.SeriesServicos", new[] { "ServicoStreamingID" });
            DropIndex("dbo.SeriesServicos", new[] { "SerieID" });
            DropIndex("dbo.SeriesGeneros", new[] { "GeneroID" });
            DropIndex("dbo.SeriesGeneros", new[] { "SerieID" });
            AlterColumn("dbo.Serie", "Sinopse", c => c.String(nullable: false, maxLength: 400));
            DropTable("dbo.SeriesServicos");
            DropTable("dbo.SeriesGeneros");
            CreateIndex("dbo.Serie", "ServicoStreaming_ServicoStreamingID");
            CreateIndex("dbo.Serie", "Genero_GeneroID");
            AddForeignKey("dbo.Serie", "ServicoStreaming_ServicoStreamingID", "dbo.ServicoStreaming", "ServicoStreamingID");
            AddForeignKey("dbo.Serie", "Genero_GeneroID", "dbo.Genero", "GeneroID");
        }
    }
}

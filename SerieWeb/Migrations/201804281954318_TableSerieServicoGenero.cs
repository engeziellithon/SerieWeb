namespace SerieWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableSerieServicoGenero : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Serie", "Genero_GeneroID", "dbo.Genero");
            DropForeignKey("dbo.Serie", "ServicoStreaming_ServicoStreamingID", "dbo.ServicoStreaming");
            DropIndex("dbo.Serie", new[] { "Genero_GeneroID" });
            DropIndex("dbo.Serie", new[] { "ServicoStreaming_ServicoStreamingID" });
            CreateTable(
                "dbo.SerieGeneroes",
                c => new
                    {
                        SerieGeneroID = c.Int(nullable: false, identity: true),
                        SerieID = c.Int(nullable: false),
                        GeneroID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SerieGeneroID)
                .ForeignKey("dbo.Genero", t => t.GeneroID, cascadeDelete: true)
                .ForeignKey("dbo.Serie", t => t.SerieID, cascadeDelete: true)
                .Index(t => t.SerieID)
                .Index(t => t.GeneroID);
            
            CreateTable(
                "dbo.SerieServicoes",
                c => new
                    {
                        SerieServicoID = c.Int(nullable: false, identity: true),
                        SerieID = c.Int(nullable: false),
                        ServicoID = c.Int(nullable: false),
                        Servico_ServicoStreamingID = c.Int(),
                    })
                .PrimaryKey(t => t.SerieServicoID)
                .ForeignKey("dbo.Serie", t => t.SerieID, cascadeDelete: true)
                .ForeignKey("dbo.ServicoStreaming", t => t.Servico_ServicoStreamingID)
                .Index(t => t.SerieID)
                .Index(t => t.Servico_ServicoStreamingID);
            
            CreateTable(
                "dbo.GeneroSeries",
                c => new
                    {
                        Genero_GeneroID = c.Int(nullable: false),
                        Serie_SerieID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genero_GeneroID, t.Serie_SerieID })
                .ForeignKey("dbo.Genero", t => t.Genero_GeneroID, cascadeDelete: true)
                .ForeignKey("dbo.Serie", t => t.Serie_SerieID, cascadeDelete: true)
                .Index(t => t.Genero_GeneroID)
                .Index(t => t.Serie_SerieID);
            
            CreateTable(
                "dbo.ServicoStreamingSeries",
                c => new
                    {
                        ServicoStreaming_ServicoStreamingID = c.Int(nullable: false),
                        Serie_SerieID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ServicoStreaming_ServicoStreamingID, t.Serie_SerieID })
                .ForeignKey("dbo.ServicoStreaming", t => t.ServicoStreaming_ServicoStreamingID, cascadeDelete: true)
                .ForeignKey("dbo.Serie", t => t.Serie_SerieID, cascadeDelete: true)
                .Index(t => t.ServicoStreaming_ServicoStreamingID)
                .Index(t => t.Serie_SerieID);
            
            AlterColumn("dbo.Serie", "Sinopse", c => c.String(nullable: false));
            DropColumn("dbo.Serie", "Genero_GeneroID");
            DropColumn("dbo.Serie", "ServicoStreaming_ServicoStreamingID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Serie", "ServicoStreaming_ServicoStreamingID", c => c.Int());
            AddColumn("dbo.Serie", "Genero_GeneroID", c => c.Int());
            DropForeignKey("dbo.SerieServicoes", "Servico_ServicoStreamingID", "dbo.ServicoStreaming");
            DropForeignKey("dbo.SerieServicoes", "SerieID", "dbo.Serie");
            DropForeignKey("dbo.SerieGeneroes", "SerieID", "dbo.Serie");
            DropForeignKey("dbo.SerieGeneroes", "GeneroID", "dbo.Genero");
            DropForeignKey("dbo.ServicoStreamingSeries", "Serie_SerieID", "dbo.Serie");
            DropForeignKey("dbo.ServicoStreamingSeries", "ServicoStreaming_ServicoStreamingID", "dbo.ServicoStreaming");
            DropForeignKey("dbo.GeneroSeries", "Serie_SerieID", "dbo.Serie");
            DropForeignKey("dbo.GeneroSeries", "Genero_GeneroID", "dbo.Genero");
            DropIndex("dbo.ServicoStreamingSeries", new[] { "Serie_SerieID" });
            DropIndex("dbo.ServicoStreamingSeries", new[] { "ServicoStreaming_ServicoStreamingID" });
            DropIndex("dbo.GeneroSeries", new[] { "Serie_SerieID" });
            DropIndex("dbo.GeneroSeries", new[] { "Genero_GeneroID" });
            DropIndex("dbo.SerieServicoes", new[] { "Servico_ServicoStreamingID" });
            DropIndex("dbo.SerieServicoes", new[] { "SerieID" });
            DropIndex("dbo.SerieGeneroes", new[] { "GeneroID" });
            DropIndex("dbo.SerieGeneroes", new[] { "SerieID" });
            AlterColumn("dbo.Serie", "Sinopse", c => c.String(nullable: false, maxLength: 400));
            DropTable("dbo.ServicoStreamingSeries");
            DropTable("dbo.GeneroSeries");
            DropTable("dbo.SerieServicoes");
            DropTable("dbo.SerieGeneroes");
            CreateIndex("dbo.Serie", "ServicoStreaming_ServicoStreamingID");
            CreateIndex("dbo.Serie", "Genero_GeneroID");
            AddForeignKey("dbo.Serie", "ServicoStreaming_ServicoStreamingID", "dbo.ServicoStreaming", "ServicoStreamingID");
            AddForeignKey("dbo.Serie", "Genero_GeneroID", "dbo.Genero", "GeneroID");
        }
    }
}

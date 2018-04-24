namespace SerieWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableGenerosAndServicoStreaming : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genero",
                c => new
                    {
                        GeneroID = c.Int(nullable: false, identity: true),
                        NomeGenero = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.GeneroID);
            
            CreateTable(
                "dbo.ServicoStreaming",
                c => new
                    {
                        ServicoStreamingID = c.Int(nullable: false, identity: true),
                        NomeServicoStreaming = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ServicoStreamingID);
            
            AddColumn("dbo.Serie", "Genero_GeneroID", c => c.Int());
            AddColumn("dbo.Serie", "ServicoStreaming_ServicoStreamingID", c => c.Int());
            CreateIndex("dbo.Serie", "Genero_GeneroID");
            CreateIndex("dbo.Serie", "ServicoStreaming_ServicoStreamingID");
            AddForeignKey("dbo.Serie", "Genero_GeneroID", "dbo.Genero", "GeneroID");
            AddForeignKey("dbo.Serie", "ServicoStreaming_ServicoStreamingID", "dbo.ServicoStreaming", "ServicoStreamingID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Serie", "ServicoStreaming_ServicoStreamingID", "dbo.ServicoStreaming");
            DropForeignKey("dbo.Serie", "Genero_GeneroID", "dbo.Genero");
            DropIndex("dbo.Serie", new[] { "ServicoStreaming_ServicoStreamingID" });
            DropIndex("dbo.Serie", new[] { "Genero_GeneroID" });
            DropColumn("dbo.Serie", "ServicoStreaming_ServicoStreamingID");
            DropColumn("dbo.Serie", "Genero_GeneroID");
            DropTable("dbo.ServicoStreaming");
            DropTable("dbo.Genero");
        }
    }
}

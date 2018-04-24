namespace SerieWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mudancabanco : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Temporada", "SerieID", "dbo.Serie");
            DropForeignKey("dbo.Serie", "Genero_GeneroID", "dbo.Generoes");
            DropIndex("dbo.Temporada", new[] { "SerieID" });
            DropIndex("dbo.Serie", new[] { "Genero_GeneroID" });
            AddColumn("dbo.Episodio", "SerieID", c => c.Int(nullable: false));
            CreateIndex("dbo.Episodio", "SerieID");
            AddForeignKey("dbo.Episodio", "SerieID", "dbo.Serie", "SerieID", cascadeDelete: true);
            DropColumn("dbo.Temporada", "SerieID");
            DropColumn("dbo.Serie", "Genero_GeneroID");
            DropTable("dbo.Generoes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Generoes",
                c => new
                    {
                        GeneroID = c.Int(nullable: false, identity: true),
                        NomeGenero = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.GeneroID);
            
            AddColumn("dbo.Serie", "Genero_GeneroID", c => c.Int());
            AddColumn("dbo.Temporada", "SerieID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Episodio", "SerieID", "dbo.Serie");
            DropIndex("dbo.Episodio", new[] { "SerieID" });
            DropColumn("dbo.Episodio", "SerieID");
            CreateIndex("dbo.Serie", "Genero_GeneroID");
            CreateIndex("dbo.Temporada", "SerieID");
            AddForeignKey("dbo.Serie", "Genero_GeneroID", "dbo.Generoes", "GeneroID");
            AddForeignKey("dbo.Temporada", "SerieID", "dbo.Serie", "SerieID", cascadeDelete: true);
        }
    }
}

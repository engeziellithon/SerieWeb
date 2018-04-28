namespace SerieWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionandoValidacao : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UsuarioPerfil", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UsuarioPerfil", new[] { "UserId" });
            AlterColumn("dbo.Serie", "NomeSerie", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Serie", "Imagem", c => c.String(nullable: false));
            AlterColumn("dbo.Temporada", "NomeTemporada", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Genero", "NomeGenero", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.UsuarioPerfil", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.UsuarioPerfil", "UserId");
            AddForeignKey("dbo.UsuarioPerfil", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsuarioPerfil", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UsuarioPerfil", new[] { "UserId" });
            AlterColumn("dbo.UsuarioPerfil", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Genero", "NomeGenero", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Temporada", "NomeTemporada", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Serie", "Imagem", c => c.String());
            AlterColumn("dbo.Serie", "NomeSerie", c => c.String(nullable: false, maxLength: 30));
            CreateIndex("dbo.UsuarioPerfil", "UserId");
            AddForeignKey("dbo.UsuarioPerfil", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}

namespace SerieWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class perfilusuario : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UsuarioPerfil",
                c => new
                    {
                        UsuarioPerfilID = c.Int(nullable: false, identity: true),
                        SerieID = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UsuarioPerfilID)
                .ForeignKey("dbo.Serie", t => t.SerieID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.SerieID)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsuarioPerfil", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UsuarioPerfil", "SerieID", "dbo.Serie");
            DropIndex("dbo.UsuarioPerfil", new[] { "UserId" });
            DropIndex("dbo.UsuarioPerfil", new[] { "SerieID" });
            DropTable("dbo.UsuarioPerfil");
        }
    }
}

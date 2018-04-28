namespace SerieWeb.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class TableUserSerieTemporadaEpisodioGenero : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Episodio",
                c => new
                    {
                        EpisodioID = c.Int(nullable: false, identity: true),
                        NomeEpisodio = c.String(nullable: false, maxLength: 30),
                        Sinopse = c.String(maxLength: 255),
                        Video = c.String(maxLength: 255),
                        DataExibicao = c.DateTime(nullable: false),
                        TemporadaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EpisodioID)
                .ForeignKey("dbo.Temporada", t => t.TemporadaID, cascadeDelete: true)
                .Index(t => t.TemporadaID);
            
            CreateTable(
                "dbo.Temporada",
                c => new
                    {
                        TemporadaID = c.Int(nullable: false, identity: true),
                        NomeTemporada = c.String(nullable: false, maxLength: 30),
                        SerieID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TemporadaID)
                .ForeignKey("dbo.Serie", t => t.SerieID, cascadeDelete: true)
                .Index(t => t.SerieID);
            
            CreateTable(
                "dbo.Serie",
                c => new
                    {
                        SerieID = c.Int(nullable: false, identity: true),
                        NomeSerie = c.String(nullable: false, maxLength: 30),
                        Imagem = c.String(),
                        Sinopse = c.String(nullable: false, maxLength: 256),
                        Nota = c.Double(nullable: false),
                        Genero_GeneroID = c.Int(),
                    })
                .PrimaryKey(t => t.SerieID)
                .ForeignKey("dbo.Generoes", t => t.Genero_GeneroID)
                .Index(t => t.Genero_GeneroID);
            
            CreateTable(
                "dbo.Generoes",
                c => new
                    {
                        GeneroID = c.Int(nullable: false, identity: true),
                        NomeGenero = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.GeneroID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Serie", "Genero_GeneroID", "dbo.Generoes");
            DropForeignKey("dbo.Temporada", "SerieID", "dbo.Serie");
            DropForeignKey("dbo.Episodio", "TemporadaID", "dbo.Temporada");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Serie", new[] { "Genero_GeneroID" });
            DropIndex("dbo.Temporada", new[] { "SerieID" });
            DropIndex("dbo.Episodio", new[] { "TemporadaID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Generoes");
            DropTable("dbo.Serie");
            DropTable("dbo.Temporada");
            DropTable("dbo.Episodio");
        }
    }
}

namespace SerieWeb.Migrations
{
    
    using System.Data.Entity.Migrations;
    
    public partial class serieFavoritaPerfil : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UsuarioPerfil", "SerieFavorita", c => c.Boolean(nullable: false));
            AddColumn("dbo.UsuarioPerfil", "InteresseSerie", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UsuarioPerfil", "InteresseSerie");
            DropColumn("dbo.UsuarioPerfil", "SerieFavorita");
        }
    }
}

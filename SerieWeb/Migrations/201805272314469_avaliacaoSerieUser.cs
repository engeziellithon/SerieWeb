namespace SerieWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class avaliacaoSerieUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UsuarioPerfil", "Avaliacao", c => c.Double(nullable: false));
            DropColumn("dbo.UsuarioPerfil", "InteresseSerie");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UsuarioPerfil", "InteresseSerie", c => c.Boolean(nullable: false));
            DropColumn("dbo.UsuarioPerfil", "Avaliacao");
        }
    }
}

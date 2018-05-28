namespace SerieWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class avaliacaoSerieTipocampo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UsuarioPerfil", "Avaliacao", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UsuarioPerfil", "Avaliacao", c => c.Double(nullable: false));
        }
    }
}

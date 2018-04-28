namespace SerieWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionandoValidacaoEpisodio : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Episodio", "NomeEpisodio", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Episodio", "Sinopse", c => c.String(maxLength: 400));           
        }
        
        public override void Down()
        {           
            AlterColumn("dbo.Episodio", "Sinopse", c => c.String(maxLength: 255));
            AlterColumn("dbo.Episodio", "NomeEpisodio", c => c.String(nullable: false, maxLength: 30));
        }
    }
}

namespace SerieWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NumEpisodioeTemporada : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Episodio", "NumeroEpisodio", c => c.Int(nullable: false));
            AddColumn("dbo.Temporada", "NumeroTemporada", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Temporada", "NumeroTemporada");
            DropColumn("dbo.Episodio", "NumeroEpisodio");
        }
    }
}

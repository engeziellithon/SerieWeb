namespace SerieWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sinopseEpisodio : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Episodio", "Sinopse", c => c.String(maxLength: 2000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Episodio", "Sinopse", c => c.String(maxLength: 400));
        }
    }
}

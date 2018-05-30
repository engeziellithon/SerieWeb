namespace SerieWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImgFavoritos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Serie", "ImagemFavorito", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Serie", "ImagemFavorito");
        }
    }
}

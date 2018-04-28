namespace SerieWeb.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class seriemodificacaoImagenBanner : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Serie", "ImagemBanner", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Serie", "ImagemBanner");
        }
    }
}

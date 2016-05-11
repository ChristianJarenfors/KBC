namespace KBC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SOMESHITERROR : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SerieGenres", "SerieId", "dbo.Series");
            DropForeignKey("dbo.SerieImgURLs", "SerieId", "dbo.Series");
            AddForeignKey("dbo.SerieGenres", "SerieId", "dbo.Series", "SerieId", cascadeDelete: true);
            AddForeignKey("dbo.SerieImgURLs", "SerieId", "dbo.Series", "SerieId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SerieImgURLs", "SerieId", "dbo.Series");
            DropForeignKey("dbo.SerieGenres", "SerieId", "dbo.Series");
            AddForeignKey("dbo.SerieImgURLs", "SerieId", "dbo.Series", "SerieId");
            AddForeignKey("dbo.SerieGenres", "SerieId", "dbo.Series", "SerieId");
        }
    }
}

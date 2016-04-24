namespace KBC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TriestoaddTheURLtables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SerieImgURLs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImgURL = c.String(),
                        Serie_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Series", t => t.Serie_Id)
                .Index(t => t.Serie_Id);
            
            CreateTable(
                "dbo.SerieVideoURLs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VideoURL = c.String(),
                        Serie_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Series", t => t.Serie_Id)
                .Index(t => t.Serie_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SerieVideoURLs", "Serie_Id", "dbo.Series");
            DropForeignKey("dbo.SerieImgURLs", "Serie_Id", "dbo.Series");
            DropIndex("dbo.SerieVideoURLs", new[] { "Serie_Id" });
            DropIndex("dbo.SerieImgURLs", new[] { "Serie_Id" });
            DropTable("dbo.SerieVideoURLs");
            DropTable("dbo.SerieImgURLs");
        }
    }
}

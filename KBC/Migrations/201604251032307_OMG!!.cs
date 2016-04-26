namespace KBC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OMG : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SerieGenres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Genre = c.Int(nullable: false),
                        SerieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Series", t => t.SerieId, cascadeDelete: true)
                .Index(t => t.SerieId);
            
            CreateTable(
                "dbo.Series",
                c => new
                    {
                        SerieId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ReleaseDatum = c.DateTime(nullable: false),
                        NumberOfVotes = c.Int(nullable: false),
                        AverageGrade = c.Single(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.SerieId);
            
            CreateTable(
                "dbo.SerieImgURLs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImgURL = c.String(),
                        Serie_SerieId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Series", t => t.Serie_SerieId)
                .Index(t => t.Serie_SerieId);
            
            CreateTable(
                "dbo.SerieVideoURLs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VideoURL = c.String(),
                        Serie_SerieId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Series", t => t.Serie_SerieId)
                .Index(t => t.Serie_SerieId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SerieGenres", "SerieId", "dbo.Series");
            DropForeignKey("dbo.SerieVideoURLs", "Serie_SerieId", "dbo.Series");
            DropForeignKey("dbo.SerieImgURLs", "Serie_SerieId", "dbo.Series");
            DropIndex("dbo.SerieVideoURLs", new[] { "Serie_SerieId" });
            DropIndex("dbo.SerieImgURLs", new[] { "Serie_SerieId" });
            DropIndex("dbo.SerieGenres", new[] { "SerieId" });
            DropTable("dbo.SerieVideoURLs");
            DropTable("dbo.SerieImgURLs");
            DropTable("dbo.Series");
            DropTable("dbo.SerieGenres");
        }
    }
}

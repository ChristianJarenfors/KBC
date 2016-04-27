namespace KBC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
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
                        Creator = c.String(),
                        ReleaseDatum = c.DateTime(nullable: false),
                        Description = c.String(),
                        NumberOfVotes = c.Int(nullable: false),
                        AverageGrade = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.SerieId);
            
            CreateTable(
                "dbo.SerieImgURLs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SerieId = c.Int(nullable: false),
                        ImgType = c.Int(nullable: false),
                        ImgURL = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Series", t => t.SerieId, cascadeDelete: true)
                .Index(t => t.SerieId);
            
            CreateTable(
                "dbo.SerieVideoURLs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SerieId = c.Int(nullable: false),
                        VideoURL = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Series", t => t.SerieId, cascadeDelete: true)
                .Index(t => t.SerieId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SerieVideoURLs", "SerieId", "dbo.Series");
            DropForeignKey("dbo.SerieImgURLs", "SerieId", "dbo.Series");
            DropForeignKey("dbo.SerieGenres", "SerieId", "dbo.Series");
            DropIndex("dbo.SerieVideoURLs", new[] { "SerieId" });
            DropIndex("dbo.SerieImgURLs", new[] { "SerieId" });
            DropIndex("dbo.SerieGenres", new[] { "SerieId" });
            DropTable("dbo.SerieVideoURLs");
            DropTable("dbo.SerieImgURLs");
            DropTable("dbo.Series");
            DropTable("dbo.SerieGenres");
        }
    }
}

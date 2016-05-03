namespace KBC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Testing : DbMigration
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
                .ForeignKey("dbo.Series", t => t.SerieId)
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
                .ForeignKey("dbo.Series", t => t.SerieId)
                .Index(t => t.SerieId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        ProfilePictureUrl = c.String(),
                        Age = c.Int(nullable: false),
                        Country = c.String(),
                        Gender = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserSeries",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Serie_SerieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Serie_SerieId })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Series", t => t.Serie_SerieId, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Serie_SerieId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSeries", "Serie_SerieId", "dbo.Series");
            DropForeignKey("dbo.UserSeries", "User_Id", "dbo.Users");
            DropForeignKey("dbo.SerieImgURLs", "SerieId", "dbo.Series");
            DropForeignKey("dbo.SerieGenres", "SerieId", "dbo.Series");
            DropIndex("dbo.UserSeries", new[] { "Serie_SerieId" });
            DropIndex("dbo.UserSeries", new[] { "User_Id" });
            DropIndex("dbo.SerieImgURLs", new[] { "SerieId" });
            DropIndex("dbo.SerieGenres", new[] { "SerieId" });
            DropTable("dbo.UserSeries");
            DropTable("dbo.Users");
            DropTable("dbo.SerieImgURLs");
            DropTable("dbo.Series");
            DropTable("dbo.SerieGenres");
        }
    }
}

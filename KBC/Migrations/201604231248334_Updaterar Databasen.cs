namespace KBC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdaterarDatabasen : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        GenreType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GenreType);
            
            CreateTable(
                "dbo.Series",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ReleaseDatum = c.DateTime(nullable: false),
                        NumberOfVotes = c.Int(nullable: false),
                        AverageGrade = c.Single(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            DropIndex("dbo.SerieGenres", new[] { "Genre_GenreType" });
            DropIndex("dbo.SerieGenres", new[] { "Serie_Id" });
            DropTable("dbo.SerieGenres");
            CreateTable(
                "dbo.SerieGenres",
                c => new
                    {
                        Serie_Id = c.Int(nullable: false),
                        Genre_GenreType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Serie_Id, t.Genre_GenreType })
                .ForeignKey("dbo.Series", t => t.Serie_Id, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.Genre_GenreType, cascadeDelete: true)
                .Index(t => t.Serie_Id)
                .Index(t => t.Genre_GenreType);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SerieGenres", "Genre_GenreType", "dbo.Genres");
            DropForeignKey("dbo.SerieGenres", "Serie_Id", "dbo.Series");
            DropIndex("dbo.SerieGenres", new[] { "Genre_GenreType" });
            DropIndex("dbo.SerieGenres", new[] { "Serie_Id" });
            DropTable("dbo.SerieGenres");
            DropTable("dbo.Series");
            DropTable("dbo.Genres");
        }
    }
}

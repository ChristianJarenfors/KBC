namespace KBC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TryingtoeditModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SerieGenres", "Serie_Id", "dbo.Series");
            DropForeignKey("dbo.SerieGenres", "Genre_Id", "dbo.Genres");
            DropForeignKey("dbo.SerieImgURLs", "Serie_Id", "dbo.Series");
            DropForeignKey("dbo.SerieVideoURLs", "Serie_Id", "dbo.Series");
            DropIndex("dbo.SerieGenres", new[] { "Serie_Id" });
            DropIndex("dbo.SerieGenres", new[] { "Genre_Id" });
            RenameColumn(table: "dbo.SerieImgURLs", name: "Serie_Id", newName: "Serie_SerieId");
            RenameColumn(table: "dbo.SerieVideoURLs", name: "Serie_Id", newName: "Serie_SerieId");
            RenameIndex(table: "dbo.SerieImgURLs", name: "IX_Serie_Id", newName: "IX_Serie_SerieId");
            RenameIndex(table: "dbo.SerieVideoURLs", name: "IX_Serie_Id", newName: "IX_Serie_SerieId");
            DropPrimaryKey("dbo.Series");
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
            
            AddColumn("dbo.Series", "SerieId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Series", "SerieId");
            AddForeignKey("dbo.SerieImgURLs", "Serie_SerieId", "dbo.Series", "SerieId");
            AddForeignKey("dbo.SerieVideoURLs", "Serie_SerieId", "dbo.Series", "SerieId");
            DropColumn("dbo.Series", "Id");
            DropTable("dbo.Genres");
            DropTable("dbo.SerieGenres");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SerieGenres",
                c => new
                    {
                        Serie_Id = c.Int(nullable: false),
                        Genre_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Serie_Id, t.Genre_Id });
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GenreType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Series", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.SerieVideoURLs", "Serie_SerieId", "dbo.Series");
            DropForeignKey("dbo.SerieImgURLs", "Serie_SerieId", "dbo.Series");
            DropForeignKey("dbo.SerieGenres", "SerieId", "dbo.Series");
            DropIndex("dbo.SerieGenres", new[] { "SerieId" });
            DropPrimaryKey("dbo.Series");
            DropColumn("dbo.Series", "SerieId");
            DropTable("dbo.SerieGenres");
            AddPrimaryKey("dbo.Series", "Id");
            RenameIndex(table: "dbo.SerieVideoURLs", name: "IX_Serie_SerieId", newName: "IX_Serie_Id");
            RenameIndex(table: "dbo.SerieImgURLs", name: "IX_Serie_SerieId", newName: "IX_Serie_Id");
            RenameColumn(table: "dbo.SerieVideoURLs", name: "Serie_SerieId", newName: "Serie_Id");
            RenameColumn(table: "dbo.SerieImgURLs", name: "Serie_SerieId", newName: "Serie_Id");
            CreateIndex("dbo.SerieGenres", "Genre_Id");
            CreateIndex("dbo.SerieGenres", "Serie_Id");
            AddForeignKey("dbo.SerieVideoURLs", "Serie_Id", "dbo.Series", "Id");
            AddForeignKey("dbo.SerieImgURLs", "Serie_Id", "dbo.Series", "Id");
            AddForeignKey("dbo.SerieGenres", "Genre_Id", "dbo.Genres", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SerieGenres", "Serie_Id", "dbo.Series", "Id", cascadeDelete: true);
        }
    }
}

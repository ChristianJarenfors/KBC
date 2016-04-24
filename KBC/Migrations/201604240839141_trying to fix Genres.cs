namespace KBC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tryingtofixGenres : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SerieGenres", "Genre_GenreType", "dbo.Genres");
            RenameColumn(table: "dbo.SerieGenres", name: "Genre_GenreType", newName: "Genre_Id");
            RenameIndex(table: "dbo.SerieGenres", name: "IX_Genre_GenreType", newName: "IX_Genre_Id");
            DropPrimaryKey("dbo.Genres");
            AddColumn("dbo.Genres", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Genres", "Id");
            AddForeignKey("dbo.SerieGenres", "Genre_Id", "dbo.Genres", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SerieGenres", "Genre_Id", "dbo.Genres");
            DropPrimaryKey("dbo.Genres");
            DropColumn("dbo.Genres", "Id");
            AddPrimaryKey("dbo.Genres", "GenreType");
            RenameIndex(table: "dbo.SerieGenres", name: "IX_Genre_Id", newName: "IX_Genre_GenreType");
            RenameColumn(table: "dbo.SerieGenres", name: "Genre_Id", newName: "Genre_GenreType");
            AddForeignKey("dbo.SerieGenres", "Genre_GenreType", "dbo.Genres", "GenreType", cascadeDelete: true);
        }
    }
}

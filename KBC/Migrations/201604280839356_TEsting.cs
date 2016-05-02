namespace KBC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TEsting : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SerieVideoURLs", "SerieId", "dbo.Series");
            DropIndex("dbo.SerieVideoURLs", new[] { "SerieId" });
            DropTable("dbo.SerieVideoURLs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SerieVideoURLs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SerieId = c.Int(nullable: false),
                        VideoURL = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.SerieVideoURLs", "SerieId");
            AddForeignKey("dbo.SerieVideoURLs", "SerieId", "dbo.Series", "SerieId", cascadeDelete: true);
        }
    }
}

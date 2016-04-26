namespace KBC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoveddbsetUsersfromUserContexttoSerieContext : DbMigration
    {
        public override void Up()
        {
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
            
            AddColumn("dbo.Series", "User_Id", c => c.Int());
            CreateIndex("dbo.Series", "User_Id");
            AddForeignKey("dbo.Series", "User_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Series", "User_Id", "dbo.Users");
            DropIndex("dbo.Series", new[] { "User_Id" });
            DropColumn("dbo.Series", "User_Id");
            DropTable("dbo.Users");
        }
    }
}

namespace KBC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addisadmingbool : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "isAdmin", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "isAdmin");
        }
    }
}

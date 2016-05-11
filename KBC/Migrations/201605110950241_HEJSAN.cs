namespace KBC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HEJSAN : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "isAdmin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "isAdmin", c => c.Boolean(nullable: false));
        }
    }
}

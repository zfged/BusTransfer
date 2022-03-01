namespace Bus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CoordR : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "CoordsFromR", c => c.String(nullable: false));
            AddColumn("dbo.Clients", "CoordsToR", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "CoordsToR");
            DropColumn("dbo.Clients", "CoordsFromR");
        }
    }
}

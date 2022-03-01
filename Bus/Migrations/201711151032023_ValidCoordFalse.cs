namespace Bus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValidCoordFalse : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clients", "CoordsFromR", c => c.String());
            AlterColumn("dbo.Clients", "CoordsToR", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clients", "CoordsToR", c => c.String(nullable: false));
            AlterColumn("dbo.Clients", "CoordsFromR", c => c.String(nullable: false));
        }
    }
}

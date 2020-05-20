namespace Kutuphane.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.UserBookMaps", name: "userName", newName: "userId");
            RenameIndex(table: "dbo.UserBookMaps", name: "IX_userName", newName: "IX_userId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.UserBookMaps", name: "IX_userId", newName: "IX_userName");
            RenameColumn(table: "dbo.UserBookMaps", name: "userId", newName: "userName");
        }
    }
}

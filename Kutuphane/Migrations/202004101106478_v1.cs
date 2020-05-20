namespace Kutuphane.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Password = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Isbn = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        AuthorName = c.String(nullable: false),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.Isbn);
            
            CreateTable(
                "dbo.UserBookMaps",
                c => new
                    {
                        Isbn = c.String(nullable: false, maxLength: 128),
                        userName = c.String(nullable: false, maxLength: 128),
                        deliveryDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Isbn)
                .ForeignKey("dbo.Books", t => t.Isbn)
                .ForeignKey("dbo.StandardUser", t => t.userName, cascadeDelete: true)
                .Index(t => t.Isbn)
                .Index(t => t.userName);
            
            CreateTable(
                "dbo.StandardUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Password = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserBookMaps", "userName", "dbo.StandardUser");
            DropForeignKey("dbo.UserBookMaps", "Isbn", "dbo.Books");
            DropIndex("dbo.UserBookMaps", new[] { "userName" });
            DropIndex("dbo.UserBookMaps", new[] { "Isbn" });
            DropTable("dbo.StandardUser");
            DropTable("dbo.UserBookMaps");
            DropTable("dbo.Books");
            DropTable("dbo.AdminUser");
        }
    }
}

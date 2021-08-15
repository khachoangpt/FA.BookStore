namespace FA.BookStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "common.Authors",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        InsertedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "common.Books",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(maxLength: 255),
                        Summary = c.String(maxLength: 1024),
                        ImgUrl = c.String(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        Published = c.Boolean(nullable: false),
                        CategoryId = c.Guid(nullable: false),
                        PublisherId = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        InsertedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("common.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("common.Publishers", t => t.PublisherId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.PublisherId);
            
            CreateTable(
                "common.Categories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        InsertedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "common.Publishers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        InsertedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "common.Reviews",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BookId = c.Guid(nullable: false),
                        Content = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        InsertedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("common.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId);
            
            CreateTable(
                "common.BookAuthorMap",
                c => new
                    {
                        BookId = c.Guid(nullable: false),
                        AuthorId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookId, t.AuthorId })
                .ForeignKey("common.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("common.Authors", t => t.AuthorId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.AuthorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("common.Reviews", "BookId", "common.Books");
            DropForeignKey("common.Books", "PublisherId", "common.Publishers");
            DropForeignKey("common.Books", "CategoryId", "common.Categories");
            DropForeignKey("common.BookAuthorMap", "AuthorId", "common.Authors");
            DropForeignKey("common.BookAuthorMap", "BookId", "common.Books");
            DropIndex("common.BookAuthorMap", new[] { "AuthorId" });
            DropIndex("common.BookAuthorMap", new[] { "BookId" });
            DropIndex("common.Reviews", new[] { "BookId" });
            DropIndex("common.Books", new[] { "PublisherId" });
            DropIndex("common.Books", new[] { "CategoryId" });
            DropTable("common.BookAuthorMap");
            DropTable("common.Reviews");
            DropTable("common.Publishers");
            DropTable("common.Categories");
            DropTable("common.Books");
            DropTable("common.Authors");
        }
    }
}

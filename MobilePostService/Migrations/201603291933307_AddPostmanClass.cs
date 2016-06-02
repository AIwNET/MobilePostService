namespace MobilePostService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostmanClass : DbMigration
    {
        public override void Up()
        {   
            CreateTable(
                "dbo.Postmen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastName = c.String(maxLength: 4000),
                        FirstName = c.String(maxLength: 4000),
                        Phone = c.String(maxLength: 4000),
                        Email = c.String(maxLength: 4000),
                        City = c.String(maxLength: 4000),
                        IsConfirmed = c.Boolean(nullable: false),
                        RegistrationDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Postmen");
            DropTable("dbo.Parcels");
        }
    }
}

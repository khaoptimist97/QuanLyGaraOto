namespace QuanLyGaraOto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QLGaraV6 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserDetails", newName: "UserDetail");
            CreateTable(
                "dbo.UserType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TypeName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.UserDetail", "UserTypeID", c => c.Int(nullable: false));
            CreateIndex("dbo.UserDetail", "UserTypeID");
            AddForeignKey("dbo.UserDetail", "UserTypeID", "dbo.UserType", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserDetail", "UserTypeID", "dbo.UserType");
            DropIndex("dbo.UserDetail", new[] { "UserTypeID" });
            DropColumn("dbo.UserDetail", "UserTypeID");
            DropTable("dbo.UserType");
            RenameTable(name: "dbo.UserDetail", newName: "UserDetails");
        }
    }
}

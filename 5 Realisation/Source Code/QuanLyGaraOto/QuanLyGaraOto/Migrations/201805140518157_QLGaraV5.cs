namespace QuanLyGaraOto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QLGaraV5 : DbMigration
    {
        public override void Up()
        {
            
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TaiKhoan",
                c => new
                    {
                        IDUser = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 250),
                        Type = c.Int(),
                    })
                .PrimaryKey(t => t.IDUser);
            
        }
    }
}

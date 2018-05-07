namespace QuanLyGaraOto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QLGara43 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChiTietPhieuSua", "Deleted", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.PhieuThuTien", "Deleted", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PhieuThuTien", "Deleted");
            DropColumn("dbo.ChiTietPhieuSua", "Deleted");
        }
    }
}

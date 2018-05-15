namespace QuanLyGaraOto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QLGaraV61 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PhieuSuaChua", "TongTien", c => c.Long(nullable: false));
            AlterColumn("dbo.Xe", "TienNo", c => c.Int(nullable: false));
            AlterColumn("dbo.PhieuThuTien", "SoTienThu", c => c.Int(nullable: false));
            AlterColumn("dbo.PhuTung", "DonGiaHienHanh", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AddColumn("dbo.PhuTung", "SoLuongTon", c => c.Int());
            AlterColumn("dbo.PhuTung", "DonGiaHienHanh", c => c.Int());
            AlterColumn("dbo.PhieuThuTien", "SoTienThu", c => c.Int());
            AlterColumn("dbo.Xe", "TienNo", c => c.Int());
            AlterColumn("dbo.PhieuSuaChua", "TongTien", c => c.Long());
            DropColumn("dbo.PhuTung", "SoLuong");
        }
    }
}

namespace QuanLyGaraOto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QLGaraV7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BK_ChiTietPhieuSua",
                c => new
                    {
                        NgayBackup = c.DateTime(nullable: false, defaultValueSql:"GETDATE()" ),
                        IDPhieu = c.Int(nullable: false),
                        IDPhuTung = c.Int(nullable: false),
                        SoLuongBan = c.Int(),
                        DonGia = c.Int(),
                        IDTienCong = c.Int(nullable: false),
                        ThanhTien = c.Long(),
                        NoiDung = c.String(maxLength: 250),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.NgayBackup, t.IDPhieu, t.IDPhuTung });
            
            CreateTable(
                "dbo.BK_PhieuSuaChua",
                c => new
                    {
                        NgayBackup = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        IDPhieu = c.Int(nullable: false),
                        IDPhieuTN = c.Int(),
                        NgaySuaChua = c.DateTime(),
                        TongTien = c.Long(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.NgayBackup, t.IDPhieu });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BK_PhieuSuaChua");
            DropTable("dbo.BK_ChiTietPhieuSua");
        }
    }
}

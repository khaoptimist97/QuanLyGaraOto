namespace QuanLyGaraOto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QLGarav1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ChiTietPhieuSua", "IDTienCong", "dbo.TienCong");
            DropIndex("dbo.ChiTietPhieuSua", new[] { "IDTienCong" });
            CreateTable(
                "dbo.UserDetails",
                c => new
                    {
                        UserName = c.String(nullable: false, maxLength: 7),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.UserName);
            
            AddColumn("dbo.PhuTung", "SoLuongTon", c => c.Int());
            AddForeignKey("dbo.ChiTietPhieuSua", "IDPhieu", "dbo.PhieuSuaChua", "IDPhieu");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChiTietPhieuSua", "IDPhieu", "dbo.PhieuSuaChua");
            DropColumn("dbo.PhuTung", "SoLuongTon");
            DropTable("dbo.UserDetails");
            CreateIndex("dbo.ChiTietPhieuSua", "IDTienCong");
            AddForeignKey("dbo.ChiTietPhieuSua", "IDTienCong", "dbo.TienCong", "IDTienCong");
        }
    }
}

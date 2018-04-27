namespace QuanLyGaraOto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QLGarav21 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ChiTietPhieuSua", "IDTienCong", c => c.Int(nullable: false));
            CreateIndex("dbo.ChiTietPhieuSua", "IDTienCong");
            AddForeignKey("dbo.ChiTietPhieuSua", "IDTienCong", "dbo.TienCong", "IDTienCong", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChiTietPhieuSua", "IDTienCong", "dbo.TienCong");
            DropIndex("dbo.ChiTietPhieuSua", new[] { "IDTienCong" });
            AlterColumn("dbo.ChiTietPhieuSua", "IDTienCong", c => c.Int());
        }
    }
}

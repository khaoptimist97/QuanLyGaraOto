namespace QuanLyGaraOto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QLGarav2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ChiTietPhieuSua", "IDPhieu", "dbo.PhieuSuaChua");
        }
        
        public override void Down()
        {
            AddForeignKey("dbo.ChiTietPhieuSua", "IDPhieu", "dbo.PhieuSuaChua", "IDPhieu");
        }
    }
}

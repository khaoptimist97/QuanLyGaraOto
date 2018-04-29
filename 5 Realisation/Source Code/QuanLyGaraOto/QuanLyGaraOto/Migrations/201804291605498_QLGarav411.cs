namespace QuanLyGaraOto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QLGarav411 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TienCong", "Deleted");
            DropColumn("dbo.PhuTung", "Deleted");
            DropColumn("dbo.Xe", "Deleted");
            DropColumn("dbo.PhieuTiepNhan", "Deleted");
            DropColumn("dbo.PhieuSuaChua", "Deleted");
            AddColumn("dbo.PhieuSuaChua", "Deleted", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.PhieuTiepNhan", "Deleted", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.Xe", "Deleted", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.PhuTung", "Deleted", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.TienCong", "Deleted", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TienCong", "Deleted");
            DropColumn("dbo.PhuTung", "Deleted");
            DropColumn("dbo.Xe", "Deleted");
            DropColumn("dbo.PhieuTiepNhan", "Deleted");
            DropColumn("dbo.PhieuSuaChua", "Deleted");
        }
    }
}

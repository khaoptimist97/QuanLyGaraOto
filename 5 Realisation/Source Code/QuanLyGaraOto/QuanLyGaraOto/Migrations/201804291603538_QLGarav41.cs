namespace QuanLyGaraOto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QLGarav41 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PhieuSuaChua", "Deleted", c => c.Boolean(nullable: false, defaultValue: true));
            AddColumn("dbo.PhieuTiepNhan", "Deleted", c => c.Boolean(nullable: false, defaultValue: true));
            AddColumn("dbo.Xe", "Deleted", c => c.Boolean(nullable: false, defaultValue: true));
            AddColumn("dbo.PhuTung", "Deleted", c => c.Boolean(nullable: false, defaultValue: true));
            AddColumn("dbo.TienCong", "Deleted", c => c.Boolean(nullable: false, defaultValue: true));
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

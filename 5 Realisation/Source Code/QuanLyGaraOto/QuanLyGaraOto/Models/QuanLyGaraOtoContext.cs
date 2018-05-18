namespace QuanLyGaraOto.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public partial class QuanLyGaraOtoContext : DbContext
    {
        public QuanLyGaraOtoContext()
            : base("name=QuanLyGaraOtoContext")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ChiTietPhieuSua> ChiTietPhieuSuas { get; set; }
        public virtual DbSet<HieuXe> HieuXes { get; set; }
        public virtual DbSet<PhieuSuaChua> PhieuSuaChuas { get; set; }
        public virtual DbSet<PhieuThuTien> PhieuThuTiens { get; set; }
        public virtual DbSet<PhieuTiepNhan> PhieuTiepNhans { get; set; }
        public virtual DbSet<PhuTung> PhuTungs { get; set; }
        public virtual DbSet<ThamSo> ThamSoes { get; set; }
        public virtual DbSet<TienCong> TienCongs { get; set; }
        public virtual DbSet<Xe> Xes { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
        public virtual DbSet<BK_PhieuSuaChua>  BK_PhieuSuaChuas { get; set; }
        public virtual DbSet<BK_ChiTietPhieuSua> BK_ChiTietPhieuSuas { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuSuaChua>()
                .HasMany(e => e.ChiTietPhieuSuas)
                .WithRequired(e => e.PhieuSuaChua)
                .HasForeignKey(e => e.IDPhieu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhuTung>()
                .HasMany(e => e.ChiTietPhieuSuas)
                .WithRequired(e => e.PhuTung)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ThamSo>()
                .Property(e => e.NoiDung)
                .IsUnicode(false);

            modelBuilder.Entity<Xe>()
                .Property(e => e.DienThoai)
                .IsFixedLength();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}

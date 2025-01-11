using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DoChoiXeMay.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<ChiTietTC> ChiTietTCs { get; set; }
        public virtual DbSet<ChitietXuatNhap> ChitietXuatNhaps { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<HangHoa> HangHoas { get; set; }
        public virtual DbSet<HanhDong> HanhDongs { get; set; }
        public virtual DbSet<HinhThucTC> HinhThucTCs { get; set; }
        public virtual DbSet<KyXuatNhap> KyXuatNhaps { get; set; }
        public virtual DbSet<LoaiUserTek> LoaiUserTeks { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<MaTC> MaTCs { get; set; }
        public virtual DbSet<NhatKyUTek> NhatKyUTeks { get; set; }
        public virtual DbSet<NoteKythuat> NoteKythuats { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<UserTek> UserTeks { get; set; }
        public virtual DbSet<ThuChi> ThuChis { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Color>()
                .HasMany(e => e.ChitietXuatNhaps)
                .WithRequired(e => e.Color)
                .HasForeignKey(e => e.IDColor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HanhDong>()
                .HasMany(e => e.NoteKythuats)
                .WithRequired(e => e.HanhDong)
                .HasForeignKey(e => e.IdHanhDong)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HinhThucTC>()
                .HasMany(e => e.ChiTietTCs)
                .WithRequired(e => e.HinhThucTC)
                .HasForeignKey(e => e.IdHT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KyXuatNhap>()
                .HasMany(e => e.ChiTietTCs)
                .WithRequired(e => e.KyXuatNhap)
                .HasForeignKey(e => e.IdKyxuatnhap)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KyXuatNhap>()
                .HasMany(e => e.ChitietXuatNhaps)
                .WithRequired(e => e.KyXuatNhap)
                .HasForeignKey(e => e.IdKy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoaiUserTek>()
                .HasMany(e => e.UserTeks)
                .WithRequired(e => e.LoaiUserTek)
                .HasForeignKey(e => e.IdLoai)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Manufacturer>()
                .HasMany(e => e.ChitietXuatNhaps)
                .WithRequired(e => e.Manufacturer)
                .HasForeignKey(e => e.IDMF)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MaTC>()
                .HasMany(e => e.ChiTietTCs)
                .WithRequired(e => e.MaTC)
                .HasForeignKey(e => e.IdMa)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Size>()
                .HasMany(e => e.ChitietXuatNhaps)
                .WithRequired(e => e.Size)
                .HasForeignKey(e => e.IDSize)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserTek>()
                .HasMany(e => e.ChiTietTCs)
                .WithRequired(e => e.UserTek)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserTek>()
                .HasMany(e => e.KyXuatNhaps)
                .WithRequired(e => e.UserTek)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserTek>()
                .HasMany(e => e.NoteKythuats)
                .WithRequired(e => e.UserTek)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}

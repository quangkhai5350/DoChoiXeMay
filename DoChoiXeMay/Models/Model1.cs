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

        public virtual DbSet<aspnet_getVisitors> aspnet_getVisitors { get; set; }
        public virtual DbSet<ChiTietTC> ChiTietTCs { get; set; }
        public virtual DbSet<ChitietXuatNhap> ChitietXuatNhaps { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<GroupDuAn> GroupDuAns { get; set; }
        public virtual DbSet<HangHoa> HangHoas { get; set; }
        public virtual DbSet<HanhDong> HanhDongs { get; set; }
        public virtual DbSet<HinhThucTC> HinhThucTCs { get; set; }
        public virtual DbSet<KyXuatNhap> KyXuatNhaps { get; set; }
        public virtual DbSet<LoaiNoteTeK> LoaiNoteTeKs { get; set; }
        public virtual DbSet<LoaiUserTek> LoaiUserTeks { get; set; }
        public virtual DbSet<MailTeK> MailTeKs { get; set; }
        public virtual DbSet<MailTeKDetail> MailTeKDetails { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<MaTC> MaTCs { get; set; }
        public virtual DbSet<MsgAotu> MsgAotus { get; set; }
        public virtual DbSet<NhatKyUTek> NhatKyUTeks { get; set; }
        public virtual DbSet<NoteKythuat> NoteKythuats { get; set; }
        public virtual DbSet<ProjectDetail> ProjectDetails { get; set; }
        public virtual DbSet<ProjectTeK> ProjectTeKs { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<TrangThaiDuAn> TrangThaiDuAns { get; set; }
        public virtual DbSet<UserTek> UserTeks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Color>()
                .HasMany(e => e.ChitietXuatNhaps)
                .WithRequired(e => e.Color)
                .HasForeignKey(e => e.IDColor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Color>()
                .HasMany(e => e.HangHoas)
                .WithRequired(e => e.Color)
                .HasForeignKey(e => e.IDColor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GroupDuAn>()
                .HasMany(e => e.ProjectTeKs)
                .WithRequired(e => e.GroupDuAn)
                .HasForeignKey(e => e.GroupId)
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
                .Property(e => e.LuuKho)
                .IsFixedLength();

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

            modelBuilder.Entity<LoaiNoteTeK>()
                .HasMany(e => e.NoteKythuats)
                .WithRequired(e => e.LoaiNoteTeK)
                .HasForeignKey(e => e.LoaiNoteId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoaiUserTek>()
                .HasMany(e => e.UserTeks)
                .WithRequired(e => e.LoaiUserTek)
                .HasForeignKey(e => e.IdLoai)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MailTeK>()
                .HasMany(e => e.MailTeKDetails)
                .WithRequired(e => e.MailTeK)
                .HasForeignKey(e => e.FromIdmail)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Manufacturer>()
                .HasMany(e => e.ChitietXuatNhaps)
                .WithRequired(e => e.Manufacturer)
                .HasForeignKey(e => e.IDMF)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Manufacturer>()
                .HasMany(e => e.HangHoas)
                .WithRequired(e => e.Manufacturer)
                .HasForeignKey(e => e.IDMF)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MaTC>()
                .HasMany(e => e.ChiTietTCs)
                .WithRequired(e => e.MaTC)
                .HasForeignKey(e => e.IdMa)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProjectTeK>()
                .HasMany(e => e.ProjectDetails)
                .WithRequired(e => e.ProjectTeK)
                .HasForeignKey(e => e.ProjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Size>()
                .HasMany(e => e.ChitietXuatNhaps)
                .WithRequired(e => e.Size)
                .HasForeignKey(e => e.IDSize)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Size>()
                .HasMany(e => e.HangHoas)
                .WithRequired(e => e.Size)
                .HasForeignKey(e => e.IDSize)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TrangThaiDuAn>()
                .HasMany(e => e.ProjectTeKs)
                .WithRequired(e => e.TrangThaiDuAn)
                .HasForeignKey(e => e.TrangthaiId)
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
                .HasMany(e => e.MailTeKDetails)
                .WithRequired(e => e.UserTek)
                .HasForeignKey(e => e.toUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserTek>()
                .HasMany(e => e.NoteKythuats)
                .WithRequired(e => e.UserTek)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserTek>()
                .HasMany(e => e.ProjectDetails)
                .WithRequired(e => e.UserTek)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}

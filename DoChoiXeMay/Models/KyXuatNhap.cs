namespace DoChoiXeMay.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KyXuatNhap")]
    public partial class KyXuatNhap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KyXuatNhap()
        {
            ChiTietTCs = new HashSet<ChiTietTC>();
            ChitietXuatNhaps = new HashSet<ChitietXuatNhap>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string TenKy { get; set; }

        public int CKphantram { get; set; }

        public double CKtienmat { get; set; }

        public double Shipper { get; set; }

        public int VAT { get; set; }

        public int UserId { get; set; }

        public int IdHanhDong { get; set; }

        public int PushtoNXId { get; set; }

        public bool UPush { get; set; }

        public bool AdminXacNhan { get; set; }

        public double TongTienAuto { get; set; }

        public DateTime NgayAuto { get; set; }

        [StringLength(100)]
        public string Filesave1 { get; set; }

        [StringLength(100)]
        public string Filesave2 { get; set; }

        [StringLength(100)]
        public string Filesave3 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietTC> ChiTietTCs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChitietXuatNhap> ChitietXuatNhaps { get; set; }

        public virtual HanhDong HanhDong { get; set; }

        public virtual UserTek UserTek { get; set; }
    }
}

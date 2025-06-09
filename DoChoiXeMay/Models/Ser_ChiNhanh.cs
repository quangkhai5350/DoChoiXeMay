namespace DoChoiXeMay.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ser_ChiNhanh
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ser_ChiNhanh()
        {
            Ser_kichhoat = new HashSet<Ser_kichhoat>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string TenChiNhanh { get; set; }

        [Required]
        [StringLength(150)]
        public string DaiDien { get; set; }

        [Required]
        [StringLength(100)]
        public string SDT { get; set; }

        [StringLength(200)]
        public string DiaChi { get; set; }

        [StringLength(150)]
        public string TaiKhoanNH { get; set; }
        [StringLength(100)]
        public string Gmail { get; set; }
        public bool Sudung { get; set; }

        public int IdLevel { get; set; }

        [StringLength(1000)]
        public string GhiChu { get; set; }

        public virtual Ser_Levelchinhanh Ser_Levelchinhanh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ser_kichhoat> Ser_kichhoat { get; set; }
    }
}

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
        [StringLength(100)]
        public string TenChiNhanh { get; set; }

        [Required]
        [StringLength(100)]
        public string DaiDien { get; set; }

        public bool Sudung { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ser_kichhoat> Ser_kichhoat { get; set; }
    }
}

namespace DoChoiXeMay.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserTek")]
    public partial class UserTek
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserTek()
        {
            KyXuatNhaps = new HashSet<KyXuatNhap>();
            NoteKythuats = new HashSet<NoteKythuat>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(128)]
        public string Password { get; set; }

        [Required]
        [StringLength(128)]
        public string PasswordSalt { get; set; }

        public int IdLoai { get; set; }

        [StringLength(100)]
        public string LoaiConnection { get; set; }

        [Required]
        [StringLength(200)]
        public string EmailConnection { get; set; }

        public bool Islocked { get; set; }

        public DateTime LastLokedChangedate { get; set; }

        public DateTime lastPasswordChangedate { get; set; }

        public DateTime Createdate { get; set; }

        public int CountFailedPassword { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KyXuatNhap> KyXuatNhaps { get; set; }

        public virtual LoaiUserTek LoaiUserTek { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NoteKythuat> NoteKythuats { get; set; }
    }
}
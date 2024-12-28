namespace DoChoiXeMay.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThuChi")]
    public partial class ThuChi
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Key]
        [Column(Order = 1)]
        public double ConlaiAuto { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime TinhDenNgayAuto { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime NgayUpdateAuto { get; set; }

        [Key]
        [Column(Order = 4)]
        public double TongThuCKAuto { get; set; }

        [Key]
        [Column(Order = 5)]
        public double TongChiCKAuto { get; set; }

        [Key]
        [Column(Order = 6)]
        public double TongThuTienMatAuto { get; set; }

        [Key]
        [Column(Order = 7)]
        public double TongChiTienMatAuto { get; set; }

        [Key]
        [Column(Order = 8)]
        public double TongThuTK_TEKAuto { get; set; }

        [Key]
        [Column(Order = 9)]
        public double TongChiTK_TEKAuto { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SoLanUpdateAuto { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }
    }
}

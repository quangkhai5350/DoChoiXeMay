namespace DoChoiXeMay.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QCtrangchu")]
    public partial class QCtrangchu
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(500)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime Ngay { get; set; }

        [Key]
        [Column(Order = 3)]
        public bool Sudung { get; set; }

        [Key]
        [Column(Order = 4)]
        public bool Img { get; set; }

        [StringLength(50)]
        public string Stt { get; set; }

        [StringLength(500)]
        public string Ghichu { get; set; }
    }
}

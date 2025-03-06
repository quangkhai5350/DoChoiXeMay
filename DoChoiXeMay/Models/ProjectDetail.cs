namespace DoChoiXeMay.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProjectDetail")]
    public partial class ProjectDetail
    {
        public Guid Id { get; set; }

        public int UserId { get; set; }

        [Required]
        [StringLength(1000)]
        public string CongViec { get; set; }

        public int TrangThaiId { get; set; }

        public DateTime NgayBatDau { get; set; }

        public DateTime NgayUpdate { get; set; }

        public int ProjectId { get; set; }

        [StringLength(200)]
        public string Ghichu { get; set; }

        public bool Leader { get; set; }

        public virtual ProjectTeK ProjectTeK { get; set; }

        public virtual UserTek UserTek { get; set; }
        [ForeignKey("TrangThaiId")]
        public virtual TrangThaiDuAn TrangThaiDuAn { get; set; }
    }
}

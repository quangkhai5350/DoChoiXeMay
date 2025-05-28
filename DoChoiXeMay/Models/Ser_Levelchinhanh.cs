using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoChoiXeMay.Models
{
    public class Ser_Levelchinhanh
    {
        public Ser_Levelchinhanh()
        {
            Ser_ChiNhanhs = new HashSet<Ser_ChiNhanh>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Level_Name { get; set; }
        public int ChietKhau_bandau { get; set; }
        public int chietkhau_khbh { get; set; }
        public int chietkhau_KPIQUI { get; set; }
        public int ChietKhau_KPIYEAR { get; set; }
        public int ChietKhau_khac { get; set; }
        [StringLength(200)]
        public string Thuongcuoinam { get; set; }
        public virtual ICollection<Ser_ChiNhanh> Ser_ChiNhanhs { get; set; }
    }
}
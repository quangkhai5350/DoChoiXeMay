using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoChoiXeMay.Models
{
    public class aspnet_getVisitors
    {
        public int Id { get; set; }

        public int TongLuotTruyCap { get; set; }

        public int Online { get; set; }
        [StringLength(100)]
        public string Ghichu { get; set; }

        [Column(TypeName = "date")]
        public DateTime Ngay { get; set; }

    }
}
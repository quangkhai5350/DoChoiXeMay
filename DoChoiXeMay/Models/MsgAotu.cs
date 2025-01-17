using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoChoiXeMay.Models
{
    [Table("MsgAotu")]
    public class MsgAotu
    {
        public Guid Id { get; set; }
        public int UserIdmsgAotu { get; set; }
        [Required]
        [StringLength(500)]
        public string MsgHeThong { get; set; }
        public DateTime NgayTao { get; set; }
        public bool AdminDaxem { get; set; }
        public bool Sub2Daxem { get; set; }
        public bool Sub4Daxem { get; set; }
        public bool Sub5Daxem { get; set; }
        public bool Sub6Daxem { get; set; }
    }
}
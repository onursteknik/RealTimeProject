using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTP.DAL
{
    [Table("InformationReport")]
    public class InformationReport
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        public int? ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string NumberOfResults { get; set; }
        public int? StopID { get; set; }
        public DateTime? TimeStamp { get; set; }
        public string Results { get; set; }
    }
}

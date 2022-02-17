using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdeasAndInvestors.Models
{
    public class DonorMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Did { get; set; }

        public int Iid { get; set; }

        [Column(TypeName = "varchar(50)")]
        public DateTime Ddate { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Damount { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Dcomment { get; set; }
    }
}

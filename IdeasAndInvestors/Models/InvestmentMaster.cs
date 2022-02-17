using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdeasAndInvestors.Models
{
    public class InvestmentMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Insid { get; set; }

        public int Iid { get; set; }

        public int Pid { get; set; }

        public DateTime Insdate { get; set; }

        [Column(TypeName = "varchar(50)")]
        public DateTime Instime { get; set; }

        public int Insamount { get; set; }


        [Column(TypeName = "varchar(50)")]
        public string Instype { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdeasAndInvestors.Models
{
    public class ComplainMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Compid { get; set; }

        public int Pid { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Cdetails { get; set; }

    }
}

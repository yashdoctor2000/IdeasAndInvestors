using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdeasAndInvestors.Models
{
    public class QuestionMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Qid { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Questiontext { get; set; }
    }
}

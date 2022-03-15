using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdeasAndInvestors.Models
{
    public class CategoryMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Catid { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Catname { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Catimage { get; set; }
    }
}

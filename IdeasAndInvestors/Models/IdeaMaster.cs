using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdeasAndInvestors.Models
{
    public class IdeaMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Iid { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Ititle { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Idescription { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string IinvestmentNeeded { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string IinvestmentDuration { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Iimage { get; set; }

        [Column(TypeName = "varchar(50)")]
        public DateTime Idate { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Ividurl { get; set; }

        
        public int Pid { get; set; }

        
        public int Catid { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string IRFLT10Pnt { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string IRFLT20Pnt { get; set; }

    }
}

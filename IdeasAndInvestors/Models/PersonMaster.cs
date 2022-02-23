using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdeasAndInvestors.Models
{
    public class PersonMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Pid { get; set; }
        
        [Column(TypeName ="varchar(50)")]
        public string Pname { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Paddress { get; set; }

        [Column(TypeName = "varchar(50)")]
        public DateTime Pdob { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Pgender { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Pphone { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Pqualification { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Pemail { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Ppassword { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Pimage { get; set; }

       
        public int Pqid { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Panswer { get; set; }

        
        public int Prollid { get; set; }



    }
}

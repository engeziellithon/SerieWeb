using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SerieWeb.Models.Admininstracao
{
    [Table("SeriesGeneros")]
    public class SeriesGeneros
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]        
        public int SerieGenerosID { get; set; }

        public int SerieID { get; set; }        
        public virtual Serie Serie { get; set; }
               
        public int GeneroID { get; set; }       
        public virtual Genero Genero { get; set; }
    }
}
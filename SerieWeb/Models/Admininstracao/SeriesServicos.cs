using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SerieWeb.Models.Admininstracao
{
    [Table("SeriesServicos")]
    public class SeriesServicos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]        
        public int SerieServicosID { get; set; }        
        
        public int SerieID { get; set; }       
        public virtual Serie Serie { get; set; }       
       
        public int ServicoStreamingID { get; set; }        
        public virtual ServicoStreaming ServicoStreaming { get; set; }
    }
}
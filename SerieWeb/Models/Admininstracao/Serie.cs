using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SerieWeb.Models.Admininstracao
{
    [Table(name: "Serie")]
    public class Serie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SerieID { get; set; }

        // http://www.macoratti.net/13/12/c_vdda.htm
        [Required()]
        [MaxLength(30)]
        [Display(Name = "Nome da Série")]
        public String NomeSerie { get; set; }

        public String Imagem { get; set; }

        public String ImagemBanner { get; set; }

        [DataType(DataType.MultilineText)]
        [Required()]        
        public String Sinopse { get; set; }

        public double Nota { get; set; }

        //[ForeignKey("SerieID")]
        //public ICollection<Episodio> Episodio { get; set; }
    }
}

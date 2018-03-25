using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeriesWeb.Models
{
    [Table(name: "Serie")]
    public class Serie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SerieID { get; set; }

        [Required()]
        [MaxLength(30)]
        public String NomeSerie { get; set; }

        public String Imagem { get; set; }

        [Required()]
        [MaxLength(256)]
        public String Sinopse { get; set; }

        public double Nota { get; set; }
    }
}

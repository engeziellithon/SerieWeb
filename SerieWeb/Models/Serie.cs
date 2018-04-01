using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SerieWeb.Models
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

        public String ImagemBanner { get; set; }

        [Required()]
        [MaxLength(1000)]
        public String Sinopse { get; set; }

        public double Nota { get; set; }
    }
}

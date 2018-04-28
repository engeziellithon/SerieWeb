using System;
using System.Collections.Generic;
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

       
        [Required()]
        [StringLength(40, MinimumLength = 2)]
        [Display(Name = "Nome da Série")]
        public String NomeSerie { get; set; }

        [Required()]
        [Url]
        [DataType(DataType.ImageUrl)]
        public String Imagem { get; set; }

        [RegularExpression("https://www.youtube.com/watch\\?v=.*", ErrorMessage = "Adicione uma Url do Youtube")]
        public String Trailer { get; set; }

        [DataType(DataType.MultilineText)]
        [Required()]        
        public String Sinopse { get; set; }

        [Range(1, 100)]
        public double Nota { get; set; }

        public virtual ICollection<Genero> generos { get; set; }

        public virtual ICollection<ServicoStreaming> servicoStreaming { get; set; }
    }
}

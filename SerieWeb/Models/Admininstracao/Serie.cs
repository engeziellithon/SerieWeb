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

        [RegularExpression(@"^(https:\/\/)youtu(.be)?(\.com)?\/.+", ErrorMessage = "Adicione Url de Compartilhamento do Youtube.Ex:"+""+"https:/ /youtu.be/4SZ3rMMYBLY"+"")]
        public String Trailer { get; set; }

        [DataType(DataType.MultilineText)]
        [Required()]        
        public String Sinopse { get; set; }

        [Range(1, 100)]
        public double Nota { get; set; }

        [NotMapped]
        public int[] ListGeneros { get; set; }

        [NotMapped]
        public int[] ListServicos { get; set; }
    }
}

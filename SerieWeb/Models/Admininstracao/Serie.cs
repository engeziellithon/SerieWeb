﻿using System;
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
       
        
        [Required(ErrorMessage = "O campo Nome da Série é obrigatório.")]
        [StringLength(40, MinimumLength = 2)]
        [Display(Name = "Nome da Série")]
        public String NomeSerie { get; set; }

        [Required(ErrorMessage = "O campo Imagem é obrigatório.")]
        [Url]
        [DataType(DataType.ImageUrl)]
        public String Imagem { get; set; }

        [Required(ErrorMessage = "O campo Banner é obrigatório.")]
        [Url]
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Banner")]
        public String ImagemFavorito { get; set; }

        [RegularExpression(@"^(https:\/\/)youtu(.be)?(\.com)?\/.+", ErrorMessage = "Adicione Url de Compartilhamento do Youtube. Ex:"+""+"https:/ /youtu.be/4SZ3rMMYBLY"+"")]
        public String Trailer { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "O campo Sinopse é obrigatório.")]
        public String Sinopse { get; set; }       

        [NotMapped]
        [Display(Name = "Gêneros")]
        [Required(ErrorMessage = "A série precisa ter pelo menos 1 gênero")]
        public int[] ListGeneros { get; set; }

        [Display(Name = "Serviços de Streaming")]
        [NotMapped]
        public int[] ListServicos { get; set; }
    }
}

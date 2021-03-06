﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SerieWeb.Models.Admininstracao
{
    [Table(name: "Genero")]
    public class Genero
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GeneroID { get; set; }

        [Required(ErrorMessage = "O campo Gênero é obrigatório.")]
        [Display(Name = "Gênero")]
        [StringLength(40, MinimumLength = 4)]
        [RegularExpression(@"^[a-zA-Z]{1,40}$", ErrorMessage = "Números e caracteres especiais não são permitidos no Gênero.")]
        public String NomeGenero { get; set; }

       
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SerieWeb.Models.Admininstracao
{
    [Table(name: "Genero")]
    public class Genero
    {
        public int GeneroID { get; set; }

        [Required()]
        [Display(Name = "Gênero")]
        [MaxLength(100)]
        public String NomeGenero { get; set; }

        public ICollection<Serie> Series { get; set; }
    }
}
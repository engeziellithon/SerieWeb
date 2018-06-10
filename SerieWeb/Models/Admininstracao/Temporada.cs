using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SerieWeb.Models.Admininstracao
{
    [Table(name: "Temporada")]
    public class Temporada 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TemporadaID { get; set; }

        [Required(ErrorMessage = "O campo Nome da Temporada é obrigatório.")]
        [StringLength(15, MinimumLength = 9)]
        [Display(Name = "Nome da Temporada")]
        public String NomeTemporada { get; set; }

        
    }
}

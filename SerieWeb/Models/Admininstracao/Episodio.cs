using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SerieWeb.Models.Admininstracao
{
    [Table(name:"Episodio")]
    public class Episodio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EpisodioID { get; set; }

        [Required()]       
        [StringLength(40, MinimumLength = 4)]
        [Display(Name = "Nome da Episódio")]
        public String NomeEpisodio { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(400, MinimumLength = 50)]
        public string Sinopse { get; set; }

        [Url]
        [DataType(DataType.Url)]
        [MaxLength(255)]
        [RegularExpression("https://www.youtube.com/watch\\?v=.*", ErrorMessage = "Adicione uma Url do Youtube")]
        public string Video { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "mm/dd/yyyy")]
        public DateTime DataExibicao { get; set; }

        public int SerieID { get; set; }
        [ForeignKey(name: "SerieID")]
        public virtual Serie serie { get; set; }

        public int TemporadaID { get; set; }
        [ForeignKey(name: "TemporadaID")]
        public virtual Temporada Temporada { get; set; }
    }
}

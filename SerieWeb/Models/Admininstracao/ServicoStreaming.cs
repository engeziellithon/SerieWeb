using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SerieWeb.Models.Admininstracao
{
    [Table(name: "ServicoStreaming")]
    public class ServicoStreaming
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServicoStreamingID { get; set; }

        [Required()]
        [Display(Name = "Serviço Streaming")]
        [StringLength(100, MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Números e caracteres especiais não são permitidos.")]
        public String NomeServicoStreaming { get; set; }

        
    }
}
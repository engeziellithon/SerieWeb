using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SerieWeb.Models.Admininstracao
{
    [Table(name: "ServicoStreaming")]
    public class ServicoStreaming
    {
        public int ServicoStreamingID { get; set; }

        [Required()]
        [Display(Name = "Serviço Streaming")]
        [MaxLength(100)]
        public String NomeServicoStreaming { get; set; }

        public ICollection<Serie> Series { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SerieWeb.Models.Admininstracao;
using SerieWeb.Models.Identity;

namespace SerieWeb.Models.Usuario
{

    [Table(name: "UsuarioPerfil")]
    public class UsuarioPerfil
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UsuarioPerfilID { get; set; }

        [Required()]
        public int SerieID { get; set; }
        [ForeignKey(name: "SerieID")]
        public virtual Serie Serie { get; set; }

        [Required()]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public bool SerieFavorita { get; set; }

        public bool InteresseSerie { get; set; }
    }
}
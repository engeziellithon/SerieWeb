using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;
using SerieWeb.Models.Identity;


namespace SerieWeb.Models
{

    [Table(name: "UsuarioPerfil")]
    public class UsuarioPerfil
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UsuarioPerfilID { get; set; }

        public int SerieID { get; set; }
        [ForeignKey(name: "SerieID")]
        public virtual Serie Serie { get; set; }
       
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
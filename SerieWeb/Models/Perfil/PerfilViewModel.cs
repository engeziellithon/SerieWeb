using SerieWeb.Migrations;
using System.Collections.Generic;

namespace SerieWeb.Models.Perfil
{
    public class PerfilViewModel
    {
        public IEnumerable<Serie> serie { get; set; }

        public IEnumerable<UsuarioPerfil> usuarioPerfil { get; set; }
    }
}
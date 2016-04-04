using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.DataAnnotations;

namespace GestionClaves.Modelos.Entidades
{
    [Alias("Usuarios")]
    public class Usuario
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Contrasena { get; set; }
        [Alias("Nombre_Completo")]
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        [Alias("Id_Perfil")]
        public int? IdPerfil { get; set; }
        [Alias("Id_Sucursal")]
        public int? IdSucursal { get; set; }
        public bool? Activo { get; set; }

    }
}

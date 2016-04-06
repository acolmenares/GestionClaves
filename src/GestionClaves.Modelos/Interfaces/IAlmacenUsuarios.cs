﻿using GestionClaves.Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionClaves.Modelos.Interfaces
{
    public interface IAlmacenUsuarios
    {
       
        Usuario ConsultarPorLogin(string login);
        int ActualizarClave(Usuario usuario);
       
    }
}

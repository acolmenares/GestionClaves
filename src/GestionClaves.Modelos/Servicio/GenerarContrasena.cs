﻿using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionClaves.Modelos.Servicio
{
    public class GenerarContrasena:IReturn<GenerarContrasenaResponse>
    {
        public string Login { get; set; }
    }


    public class GenerarContrasenaResponse : IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
    }
}

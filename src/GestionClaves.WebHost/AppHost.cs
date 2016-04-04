using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Funq;
using GestionClaves.Servicio;
using ServiceStack.Configuration;
using ServiceStack.OrmLite;
using GestionClaves.DAL;
using GestionClaves.BL.Validadores;
using GestionClaves.BL.Gestores;
using GestionClaves.Modelos.Interfaces;

namespace GestionClaves.WebHost
{
    public class AppHost : AppHostBase
    {
        public AppHost() : base("Gestión de Contraseñas", typeof(ServicioBase).Assembly) { }

        public override void Configure(Container container)
        {
            SetConfig(new HostConfig
            {
                DebugMode = true,
                HandlerFactoryPath = "gc-api",
            });
                        
            Plugins.Add(new CorsFeature());

            var appSettings = new AppSettings();
                        
            var varConexionBDSeguridad = appSettings.Get<string>("ConexionBDSegurida", "APP_CONEXION_IRD_SEGURIDAD");
            var conexionBDSeguridad = Environment.GetEnvironmentVariable(varConexionBDSeguridad);
            var dbfactory = new OrmLiteConnectionFactory(conexionBDSeguridad, SqlServerDialect.Provider);

            var almacenUsuario = new AlmacenUsuarios(dbfactory);
            var validadorUsuarios = new ValidadorGestorUsuarios();
            var gestorUsuarios = new GestorUsuarios() { AlmacenUsuarios= almacenUsuario, ValidadorGestorUsuarios=validadorUsuarios};

            container.Register<IGestorUsuarios>(gestorUsuarios);


        }
    }
}
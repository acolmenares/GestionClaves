using ServiceStack;
using ServiceStack.Text;
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
using GestionClaves.BL.Utiles;
using GestionClaves.Modelos.Config;

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

            var conexionBDSeguridad = appSettings.Get("ConexionBDSegurida", Environment.GetEnvironmentVariable("APP_CONEXION_IRD_SEGURIDAD"));
            
            var dbfactory = new OrmLiteConnectionFactory(conexionBDSeguridad, SqlServerDialect.Provider);

            var almacenUsuario = new AlmacenUsuarios(dbfactory);
            var validadorUsuarios = new ValidadorGestorUsuarios();
            var proveedorHash = new ProveedorHash();

            var varMgConfig = appSettings.Get("MailGunConfig", Environment.GetEnvironmentVariable("APP_MAILGUNCONFIG"));
            var mgConfig =
                TypeSerializer.DeserializeFromString<MailGunConfig>(varMgConfig);
                                   
            
            var correo = new MailGunCorreo() { Config = mgConfig };
            var gestorUsuarios = new GestorUsuarios
            {
                AlmacenUsuarios = almacenUsuario,
                ValidadorGestorUsuarios = validadorUsuarios,
                ProveedorHash= proveedorHash,
                Correo=correo
                
            };

            container.Register<IGestorUsuarios>(gestorUsuarios);


        }
    }
}
using ServiceStack;
using ServiceStack.Text;
using System;
using Funq;
using GestionClaves.Servicio;
using ServiceStack.Configuration;
using ServiceStack.OrmLite;
using GestionClaves.DAL;
using GestionClaves.BL.Validadores;
using GestionClaves.BL.Gestores;
using GestionClaves.Modelos.Interfaces;
using GestionClaves.Modelos.Servicio;
using GestionClaves.BL.Utiles;
using GestionClaves.Modelos.Config;
using ServiceStack.MiniProfiler.Data;
using ServiceStack.MiniProfiler;
using System.Net.Mime;

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
                GlobalResponseHeaders =
                    {
                        { "Access-Control-Allow-Origin", "*" },
                        { "Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS, PATCH" },
                    },
                DefaultContentType = "json"
            });

            

            Plugins.Add(new CorsFeature());
            Plugins.Add(new SessionFeature()); // TODO : PONER REDIS AQUI!

            //Routes.Add<SolicitarGeneracionContrasena>("/contrasena/solicitargeneracion");
            //Routes.Add<GenerarCaptcha>("/contrasena/generarcaptcha");
            var appSettings = new AppSettings();

            var conexionBDSeguridad = appSettings.Get("ConexionBDSegurida", Environment.GetEnvironmentVariable("APP_CONEXION_IRD_SEGURIDAD"));
            
            var dbfactory = new OrmLiteConnectionFactory(conexionBDSeguridad, SqlServerDialect.Provider)
            {
                ConnectionFilter = x => new ProfiledDbConnection(x, Profiler.Current)
            };

            var fabricaConexiones = new FabricaConexiones(dbfactory);
            var repoUsuario = new RepoUsuario();
            var validadorUsuarios = new ValidadorGestorUsuarios();
            var proveedorHash = new ProveedorHash();
            var valores = new ProveedorValores();

            var varMgConfig = appSettings.Get("MailGunConfig", Environment.GetEnvironmentVariable("APP_MAILGUNCONFIG"));
            var mgConfig = TypeSerializer.DeserializeFromString<MailGunConfig>(varMgConfig);
                                   
            
            var correo = new MailGunCorreo() { Config = mgConfig };
            var gestorUsuarios = new GestorUsuarios
            {
                FabricaConexiones = fabricaConexiones,
                ValidadorGestorUsuarios = validadorUsuarios,
                ProveedorHash= proveedorHash,
                Correo=correo,
                RepoUsuario= repoUsuario,
                Valores= valores,
            };

            container.Register<IGestorUsuarios>(gestorUsuarios);
            container.Register<IProveedorValores>(valores);

        }
    }
}//src="data:image/png;base64,
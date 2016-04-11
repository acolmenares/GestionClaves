using GestionClaves.Modelos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionClaves.Modelos.Entidades;
using GestionClaves.Modelos.Config;
using RestSharp;
using RestSharp.Authenticators;

namespace GestionClaves.BL.Utiles
{
    public class MailGunCorreo : ICorreo
    {
        public MailGunConfig Config { get; set; }

        public MailResponse EnviarNotificacionGeneracionContrasena(Usuario usuario, string nuevaContrasena)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri(Config.Uri);
            client.Authenticator =
                    new HttpBasicAuthenticator("api",
                                               Config.SecretApiKey);
            RestRequest request = new RestRequest();
            request.AddParameter("domain",
                                 Config.Domain, ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", Config.From);
            request.AddParameter("to", usuario.Email);
            request.AddParameter("subject", "Contraseña Generada");
            request.AddParameter("text", nuevaContrasena);
            request.Method = Method.POST;
            var r =client.Execute<MailGunResponse>(request);
            
            return ConvertirACorreoResponse(r);
        }

        public MailResponse EnviarNotificacionActualizacionContrasena(Usuario usuario)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri(Config.Uri);
            client.Authenticator =
                    new HttpBasicAuthenticator("api",
                                               Config.SecretApiKey);
            RestRequest request = new RestRequest();
            request.AddParameter("domain",
                                 Config.Domain, ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", Config.From);
            request.AddParameter("to", usuario.Email);
            request.AddParameter("subject", "Contraseña Actualizada");
            request.AddParameter("text", "Su contraseña ha sido actualizada");
            request.Method = Method.POST;
            var r = client.Execute<MailGunResponse>(request);
            return ConvertirACorreoResponse(r);
        }

        public MailResponse EnviarTokenGeneracionContrasena(Usuario usuario)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri(Config.Uri);
            client.Authenticator =
                    new HttpBasicAuthenticator("api",
                                               Config.SecretApiKey);
            RestRequest request = new RestRequest();
            request.AddParameter("domain",
                                 Config.Domain, ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", Config.From);
            request.AddParameter("to", usuario.Email);
            request.AddParameter("subject", "Solicitud Generación nueva contraseña");
            request.AddParameter("text", usuario.Token);
            request.Method = Method.POST;
            var r = client.Execute<MailGunResponse>(request);

            return ConvertirACorreoResponse(r);
        }


        private MailResponse ConvertirACorreoResponse(IRestResponse<MailGunResponse> response)
        {
            var r = new MailResponse();
            if (response.Data != default(MailGunResponse))
            {
                r.Id = response.Data.id;
                r.Message = response.Data.message;
                
            }
            else
            {
                r.Id = "";
                r.Message = response.Content;
            }
            r.StatusCode = response.StatusCode;
            r.StatusDescription = response.StatusDescription;
            r.ErrorMessage = response.ErrorMessage;
            switch (response.ResponseStatus)
            {
                case ResponseStatus.None:
                    r.Status = MailResponseStatus.None;
                    break;
                case ResponseStatus.Completed:
                    r.Status = MailResponseStatus.Completed;
                    break;
                case ResponseStatus.Error:
                    r.Status = MailResponseStatus.Error;
                    break;
                case ResponseStatus.TimedOut:
                    r.Status = MailResponseStatus.TimedOut;
                    break;
                case ResponseStatus.Aborted:
                    r.Status = MailResponseStatus.Aborted;
                    break;
                default:
                    break;
            }
            return r;
        }

        

        class MailGunResponse {
            public string id { get; set; }
            public string message { get; set; }
        }

    }
}

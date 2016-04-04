using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GestionClaves.Modelos.Config
{
    public class CorreoResponse
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public string ErrorMessage { get; set; }
        public CorreoResponseStatus Status { get; set; }
    }


    public enum CorreoResponseStatus
    {
        None = 0,
        Completed = 1,
        Error = 2,
        TimedOut = 3,
        Aborted = 4
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GestionClaves.Modelos.Config
{
    public class MailResponse
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public string ErrorMessage { get; set; }
        public MailResponseStatus Status { get; set; }
    }


    public enum MailResponseStatus
    {
        None = 0,
        Completed = 1,
        Error = 2,
        TimedOut = 3,
        Aborted = 4
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionClaves.Modelos.Config
{
    public class MailGunConfig
    {
        public string Uri { get; set; }
        public string SecretApiKey { get; set; }
        public string Domain { get; set; }
        public string From { get; set; }
    }
}

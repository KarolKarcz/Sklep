using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Helpers
{
    public static class EmailTemplateHelpers
    {
        public static StringBuilder GetPasswordConfimartionTemplate()
        {
            StringBuilder template = new();

            template.AppendLine("<h1>Witamy w sklepie Gabio.pl</h1>");
            template.AppendLine("Aby dokończyć rejestracje kliknij w poniższy link:");
            template.AppendLine("</br></br> @Model.ActivationLink");

            return template;
        }

        public static StringBuilder GetPasswordResetTemplate()
        {
            StringBuilder template = new();

            template.AppendLine("<h1>Witaj @Model.Username</h1>");
            template.AppendLine("Otrzymaliśmy prośbę o reset hasła powiązanego z twoim adresem email. Aby zresetować hasło  kliknij w link poniżej:");
            template.AppendLine("</br></br> @Model.ResetLink");

            return template;
        }
    }
}

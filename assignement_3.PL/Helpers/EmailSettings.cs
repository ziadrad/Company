using System.Net;
using System.Net.Mail;

namespace assignement_3.PL.Helpers
{
    public class EmailSettings
    {

        public static bool sendEmail(Email email)
        {
            try
            {
                var smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("ziadrady462@gmail.com", "qsytlnjwysazzple");
                smtp.Send("ziadrady462@gmail.com", email.To, email.subject, email.body);

                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}

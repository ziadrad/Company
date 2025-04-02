using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using assignement_3.DAL.Models;
using assignement_3.PL.Helpers;

namespace assignement_3.PL
{
    public interface IMailServices
    {
        public void sendEmail(Email email);
    }
}

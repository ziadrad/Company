using assignement_3.DAL.Models;
using Twilio.Rest.Api.V2010.Account;

namespace assignement_3.PL.Interface
{
    public interface ITwilioServices
    {
        public MessageResource SendSms(SMS sms);
    }
}

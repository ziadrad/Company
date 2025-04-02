using assignement_3.DAL.Models;
using assignement_3.PL.Interface;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace assignement_3.PL.Helpers
{
    public class TwilioService(IOptions<TwilioSettings> _options) : ITwilioServices
    {
        public MessageResource SendSms(SMS sms)
        {
            TwilioClient.Init(_options.Value.AccountSID, _options.Value.AuthToken);
            var message = MessageResource.Create(
                to:sms.To,
                body:sms.body,
                from: new Twilio.Types.PhoneNumber( _options.Value.PhoneNumber)
            );

            return message;
        }
    }
}

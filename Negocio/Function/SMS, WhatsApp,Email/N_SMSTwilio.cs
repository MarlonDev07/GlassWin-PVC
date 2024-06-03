using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Negocio.SMS__WhatsApp
{
    public class N_SMSTwilio
    {
        public bool SendSMS(string Phone, String Message) 
        {
            bool Result = false;
            try
            {
                string accountSid = "AC5ecf933f2b552033a919618391de5cab";
                string authToken = "8aa300af52740c6779c5e9f229564d54";

                // Inicializa el cliente de Twilio
                TwilioClient.Init(accountSid, authToken);

                // El número de teléfono que enviará el mensaje (debes verificarlo en Twilio)
                var fromPhoneNumber = new PhoneNumber("+1 620 526 8961");

                // El número de teléfono de destino
                var toPhoneNumber = new PhoneNumber("+506"+Phone);

                // El mensaje a enviar
                string message = Message;

                // Envía el mensaje
                var messageResource = MessageResource.Create(
                    body: message,
                    from: fromPhoneNumber,
                    to: toPhoneNumber
                );
                Result  = true;
            }catch (Exception ){}


            return Result;
        }
    }
}

using CcRep.Components.Mailing;
using CcRep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CcRep.Managers.Mail
{
    public class MailManager
    {

        public async Task<bool> SendMessageFromUser(CcRepUser user, Message message)
        {
            message.AddTo("DKhachatryan.external@unicredit.ru");

            message.Body = message.Body + "<br/><br/>" + $"Отправитель: {user.FullName} ({user.Email}).";

            var sender = SokovApiMail.GetInstance();

            var result = await sender.Send(message);

            return true;
        }
    }
}
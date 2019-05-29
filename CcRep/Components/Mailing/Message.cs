using System.Collections.Generic;

namespace CcRep.Components.Mailing
{
    public class Message
    {
        public string From { get; set; } = "noreply-rad@unicredit.ru";
        public string FromName { get; set; } = "noreply-rad";

        public string Title { get; set; } = "Тестовое сообщение";
        public string Body { get; set; } = "Тестовое письмо";

        protected List<string> _to = new List<string>();
        public List<string> To {
            get {

                return _to;
            }
        }

        public void AddTo(string to)
        {
            if(!_to.Exists(t => t == to))
            {
                _to.Add(to);
            }
        }
    }
}
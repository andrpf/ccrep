using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace CcRep.Components.Mailing
{
    public class SokovApiMail
    {
        private static SokovApiMail instance;

        private SokovApiMail()
        { }

        public string ServerUri { get; set; } = "http://api.intranet/v1/api/mail";

        private readonly HttpClient client = new HttpClient();

        public async Task<string> Send(Message message)
        {
            var jsonSerialiser = new JavaScriptSerializer();
            var jsonTo = jsonSerialiser.Serialize(message.To);

            var values = new Dictionary<string, string>{
                { "Mail[to]", jsonTo },
                { "Mail[from]", message.From },
                { "Mail[from_name]", message.FromName },
                { "Mail[title]", message.Title },
                { "Mail[body]", message.Body }
            };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync(ServerUri, content);

            string responseString = await response.Content.ReadAsStringAsync();


            return responseString;
        }

        public static SokovApiMail GetInstance()
        {
            if (instance == null)
                instance = new SokovApiMail();
            return instance;
        }
    }


}
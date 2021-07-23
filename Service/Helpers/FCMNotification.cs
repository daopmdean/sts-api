using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace Service.Helpers
{
    public class FCMNotification
    {
        private readonly FCMConfiguration _config;
        private readonly string webAddr = "https://fcm.googleapis.com/fcm/send";

        public FCMNotification(FCMConfiguration config)
        {
            _config = config;
        }

        public string SendNotification(string DeviceToken, string title, string msg)
        {
            var result = "-1";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers
                .Add(string.Format("Authorization: key={0}", _config.ServerKey));
            httpWebRequest.Headers
                .Add(string.Format("Sender: id={0}", _config.SenderId));
            httpWebRequest.Method = "POST";

            var payload = new
            {
                to = DeviceToken,
                priority = "high",
                content_available = true,
                notification = new
                {
                    title,
                    body = msg
                },
            };

            using (var streamWriter =
                new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(payload);
                streamWriter.Write(json);
                streamWriter.Flush();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader =
                new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }
    }
}

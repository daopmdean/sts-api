using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using FirebaseAdmin.Messaging;
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

        public string SendNotification(
            string DeviceToken, string title, string msg)
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

        public static async Task SendNotificationAsync(
            string topic, string title, string msg)
        {
            var message = new Message()
            {
                Notification = new Notification()
                {
                    Title = title,
                    Body = msg
                },
                Topic = topic
            };

            await FirebaseMessaging.DefaultInstance.SendAsync(message);
        }

        public static async Task SubscribeToTopicAsync(string topic)
        {
            // [START subscribe_to_topic]
            // These registration tokens come from the client FCM SDKs.
            var registrationTokens = new List<string>()
            {
                "REGISTRATION_TOKEN_1",
                // ...
                "REGISTRATION_TOKEN_n",
            };

            var response = await FirebaseMessaging.DefaultInstance
                .SubscribeToTopicAsync(registrationTokens, topic);
        }

        public static async Task UnsubscribeFromTopicAsync(string topic)
        {
            // [START unsubscribe_from_topic]
            // These registration tokens come from the client FCM SDKs.
            var registrationTokens = new List<string>()
            {
                "REGISTRATION_TOKEN_1",
                // ...
                "REGISTRATION_TOKEN_n",
            };

            await FirebaseMessaging.DefaultInstance
                .UnsubscribeFromTopicAsync(registrationTokens, topic);
        }
    }
}

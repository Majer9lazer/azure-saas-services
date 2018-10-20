
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace ConnectToYandexDictionary_Http_Trig
{
    public static class YandexDictionaryFunc
    {
        private const string AppKey = "dict.1.1.20181020T041810Z.d42863121b9eda55.a404b472337bf4d8b28611a7d8eccfe4a7de39c6";
        private const string BaseUrl = "https://dictionary.yandex.net/api/v1/dicservice.json/lookup?";
        [FunctionName("YandexDictionaryFunc")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            string word = req.Query["word"];

            HttpClient client = new HttpClient();
            string reqUri = $"{BaseUrl}key={AppKey}&lang=ru-ru&text={word}";

            client.GetStringAsync(reqUri).GetAwaiter().GetResult();
            HttpResponseMessage httpResponseMessage = client.GetAsync(reqUri).GetAwaiter().GetResult();
            RootObject root = null;
           

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                try
                {
                 
                    log.Info(root.ReturnSuccessfulMessage());
                }
                catch (Exception e)
                {
                    log.Error("Внимание Ошибка!", e);
                }

            }

            return root != null
                ? (ActionResult)new OkObjectResult($"Hello, {httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult()}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
        public class Head
        {
        }

        public class Syn
        {
            public string text { get; set; }
            public string pos { get; set; }
            public override string ToString()
            {
                return $"Text = {text}";
            }
        }

        public class Tr
        {
            public string text { get; set; }
            public string pos { get; set; }
            public List<Syn> syn { get; set; }
        }

        public class Def
        {
            public string text { get; set; }
            public string pos { get; set; }
            public List<Tr> tr { get; set; }
        }

        public class RootObject
        {
            public Head head { get; set; }
            public List<Def> def { get; set; }

            public string ReturnSuccessfulMessage()
            {
                return "Инициализация прошла успешно";
            }
        }
    }
}

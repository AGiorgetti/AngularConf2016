using System.Net.Http;
using System.Text;

namespace ApplicationToMonitor
{
    public static class HttpService
    {
        static HttpClient Client = new HttpClient();

        public static void PostLogData(LogData data) {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            var uri = "http://localhost:5000/logApi/PostLogData";
            Client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
        }
    }
}
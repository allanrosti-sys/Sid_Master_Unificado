using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SID.ClickUp.Control
{
    public class API
    {
        string token;
        public API(string token)
        {
            this.token = token;
        }


        public string Get(string req_String)
        {

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", token);
            
            var request = client.GetAsync(req_String);
            string response = request.Result.Content.ReadAsStringAsync().Result;

            return response;


        }
    }
}


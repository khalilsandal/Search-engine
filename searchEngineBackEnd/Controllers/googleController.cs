using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using searchEngineBackEnd.Models;
using System.Net;

namespace searchEngineBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class googleController : ControllerBase
    {
        [HttpGet(Name = "Getgoogle")]
        public IEnumerable<searchEngineBackEnd.Models.Result> Search(string search)
        {
            string cx = "d48f883849b6f40b8";
            string apiKey = "AIzaSyDUS_wOanbDQiawa5slLwb9rIH7RItrUkc";
            var request = WebRequest.Create("https://cse.google.com/cse?cx=d48f883849b6f40b8&q=" + search);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseString = reader.ReadToEnd();
            dynamic jsonData = JsonConvert.DeserializeObject(responseString);

            var results = new List<Result>();
            foreach (var item in jsonData.items)
            {
                results.Add(new Result
                {
                    Title = item.title,
                    Link = item.link,
                    Snippet = item.snippet,
                });
            }
            return results;


        }
    }
}

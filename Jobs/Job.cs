using ApiHangFire.Models;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Jobs
{
    public class Job
    {
        public async Task JobTest(string siteExternalUrl, string apiExternalUrl, string authToken)
        {
            //get external data
            var data = await GetExternalDataFromUrl(siteExternalUrl);

            //convert external data to model ExternalData
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ExternalData));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(data));
            ExternalData externalData = (ExternalData)serializer.ReadObject(ms);

            //get events from external data
            List<Event> events = externalData.events;

            //final result from get external data
            List<FinalData> results = new List<FinalData>();

            //mapping event inside type Data
            foreach (var e in events)
            {
                var result = new FinalData();

                result.Year = e.year;
                result.Description = e.description;

                results.Add(result);
            }

            //call WebAPI to set this data in database
            await SetDataWithExternalAPI(authToken, apiExternalUrl, results);
        }
        private async Task<string> GetExternalDataFromUrl(string url)
        {
            var rnd = new Random();
            var rnd2 = new Random();

            var random = rnd.Next(1, 10).ToString();
            var random2 = rnd2.Next(1, 10).ToString();

            //set random search
            var dinamiclUrl = url.Replace("1", random).Replace("2", random2);

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(dinamiclUrl);
                //result
                var content = response.Content.ReadAsStringAsync();
                return content.Result;
            }
        }
        private async Task<object> SetDataWithExternalAPI(string authToken, string url, IEnumerable<FinalData> data)
        {
            using (var httpClient = new HttpClient())
            {
                var jsonObject = JsonConvert.SerializeObject(data);

                //jwt token
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + authToken);

                var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(url, content);
                //results
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}

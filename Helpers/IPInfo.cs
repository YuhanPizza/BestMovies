using Newtonsoft.Json;

namespace BestMovies.Helpers
{
    public class IPInfo
    {
        //what is happening:
        //these json properties, when ever we fetch this data we are hitting an api endpoint and we are fetching this data.
        //in this case i am translating the json data through this IPInfo from ipinfo.io
        //sample data
        //"ip": "142.127.186.93",
        //"hostname": "lnsm2-torontoxn-142-127-186-93.dsl.bell.ca",
        //"city": "Toronto",
        //"region": "Ontario",
        //"country": "CA",
        //"loc": "43.7064,-79.3986",
        //"org": "AS577 Bell Canada",
        //"postal": "M5A",
        //"timezone": "America/Toronto"
        [JsonProperty("ip")]
        public string Ip { get; set; }
        [JsonProperty("hostname")]
        public string Hostname { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("loc")]
        public string Location { get; set; }
        [JsonProperty("org")]
        public string Org { get; set; }
        [JsonProperty("postal")]
        public string Postal { get; set; }
        [JsonProperty("timezone")]
        public string Timezone { get; set; }
    }
}

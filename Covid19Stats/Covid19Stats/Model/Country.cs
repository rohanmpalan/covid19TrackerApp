using System;
using Newtonsoft.Json;

namespace Covid19Stats.Model
{
    public class Country
    {
        [JsonProperty("Country")]
        public string CountryName { get; set; }
        public string Slug { get; set; }
        public string CountryCode { get; set; }
        public int NewConfirmed { get; set; }
        public int TotalConfirmed { get; set; }
        public int NewDeaths { get; set; }
        public int TotalDeaths { get; set; }
        public int NewRecovered { get; set; }
        public int TotalRecovered { get; set; }

        [JsonProperty("Date")]
        public DateTime UpdatedDate { get; set; }
    }
}

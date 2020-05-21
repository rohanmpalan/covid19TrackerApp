using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Covid19Stats.Model
{
    public class CoronaStats
    {
        public GlobalStats Global{get;set;}
        public List<Country> Countries { get; set; }
        [JsonProperty("Date")]
        public DateTime UpdatedDate { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Covid19Stats.Constant
{
    public static class Endpoint
    {
        public static readonly string baseUrl = "https://api.covid19api.com/";
        public static readonly string getWorldSummary = $"{baseUrl}summary";
        public static readonly string getCountries = $"{baseUrl}countries";
        public static readonly string getWorldStats = $"{baseUrl}world/total";
        public static readonly string getCountryStats = $"{baseUrl}country/";
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InfrontAssesment.Core.Models
{
    public class PriceData
    {
        [JsonPropertyName("vwdKey")]
        public string VwdKey { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("isin")]
        public string Isin { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("time")]
        public DateTime? Time { get; set; }

        [JsonPropertyName("open")]
        public decimal? Open { get; set; }

        [JsonPropertyName("high")]
        public decimal? High { get; set; }

        [JsonPropertyName("low")]
        public decimal? Low { get; set; }

        [JsonPropertyName("close")]
        public decimal? Close { get; set; }

        [JsonPropertyName("volume")]
        public decimal? Volume { get; set; }

        [JsonPropertyName("previousClose")]
        public decimal? PreviousClose { get; set; }

        [JsonPropertyName("previousCloseTime")]
        public DateTime? PreviousCloseTime { get; set; }
    }
}

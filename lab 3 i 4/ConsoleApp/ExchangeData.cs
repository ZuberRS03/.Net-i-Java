using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class ExchangeData
    {
        [System.Text.Json.Serialization.JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("rates")]
        public Dictionary<string, decimal>? Rates { get; set; }

        public DateTime TimestampToDateTime()
        {
            return DateTimeOffset.FromUnixTimeSeconds(Timestamp).DateTime;
        }
    }
}

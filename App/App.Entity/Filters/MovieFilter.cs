using System;
using System.Text.Json.Serialization;

namespace App.Entity.Filters
{
    public class MovieFilter : BaseFilter
    {
        public int[] Actors { get; set; }
        public int[] Genres { get; set; }

        public string PriceRange { get; set; }
        [JsonIgnore]
        public decimal PriceMin { get => PriceRange != null ? Convert.ToDecimal(PriceRange.Split(';')[0]) : 0; }
        [JsonIgnore]
        public decimal PriceMax { get => PriceRange != null ? Convert.ToDecimal(PriceRange.Split(';')[1]) : 0; }
    }
}

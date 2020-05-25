using System;
using System.Collections.Generic;
using System.Text;

namespace App.Entity.Filters
{
    public class MovieFilter : BaseFilter
    {
        public int[] Actors { get; set; }
        public int[] Genres { get; set; }

        public string PriceRange { get; set; }
        public decimal PriceMin { get => PriceRange != null ? Convert.ToDecimal(PriceRange.Split(';')[0]) : 0; }
        public decimal PriceMax { get => PriceRange != null ? Convert.ToDecimal(PriceRange.Split(';')[1]) : 0; }
    }
}

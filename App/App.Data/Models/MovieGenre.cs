﻿using App.Core;

namespace App.Data.Models
{
    public class MovieGenre : BaseEntity
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }

    }
}

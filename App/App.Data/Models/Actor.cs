using App.Core;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace App.Data.Models
{
    public class Actor : BaseEntity
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        [MaxLength(128)]
        public string FullName { get; set; }

        public DateTime BirthDate { get; set; }
        public string Country { get; set; }
        public string ImdbLink { get; set; }
        public virtual Collection<Movie> Movies { get; set; }
    }
}
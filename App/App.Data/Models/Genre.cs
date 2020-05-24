﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Data.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }
    }
}
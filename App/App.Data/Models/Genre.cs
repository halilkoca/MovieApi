using App.Core;
using App.Core.DbTrackers;
using System.ComponentModel.DataAnnotations;

namespace App.Data.Models
{
    public class Genre : BaseEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }
    }
}
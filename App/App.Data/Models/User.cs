using App.Core;

namespace App.Data.Models
{
    public class User : BaseEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

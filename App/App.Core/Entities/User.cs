using System.Collections.Generic;

namespace App.Core.Entities
{
    public class User : BaseEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public string FullName { get; set; }
        public virtual ICollection<OperationClaim> OperationClaims { get; set; }
    }
}

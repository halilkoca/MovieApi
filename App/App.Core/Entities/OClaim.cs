using App.Core.DbTrackers;
using System.Collections.Generic;

namespace App.Core.Entities
{
    public class OClaim : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<UserClaim> UserClaims { get; set; }
    }
}

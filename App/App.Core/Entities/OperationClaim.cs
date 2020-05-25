using App.Core.DbTrackers;

namespace App.Core.Entities
{
    public class OperationClaim : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

namespace App.Core.Entities
{
    public class UserClaim : BaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int OClaimId { get; set; }
        public OClaim OClaim { get; set; }
    }
}

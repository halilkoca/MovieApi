using App.Core;

namespace App.Data.Models
{
    public class MovieActor : BaseEntity
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int ActorId { get; set; }
        public Actor Actor { get; set; }

    }
}

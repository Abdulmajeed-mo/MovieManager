namespace MJDVerse.Domain.Entities
{
    public class CastMember
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public Movie Movie { get; set; } = null!;

        public int PersonId { get; set; }

        public Person Person { get; set; } = null!;

        public string? CharacterName { get; set; }

        public int Order { get; set; }
    }
}
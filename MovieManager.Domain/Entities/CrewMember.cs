namespace MJDVerse.Domain.Entities
{
    public class CrewMember
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public Movie Movie { get; set; } = null!;

        public int PersonId { get; set; }

        public Person Person { get; set; } = null!;

        public string Department { get; set; } = string.Empty;

        public string Job { get; set; } = string.Empty;
    }
}
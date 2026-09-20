namespace MJDVerse.Domain.Entities
{
    public class Genre
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;



        //Many-to-Many

        public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
    }
}
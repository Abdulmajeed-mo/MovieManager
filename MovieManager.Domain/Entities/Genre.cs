namespace MJDVerse.Domain.Entities
{
    public class Genre
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;



        //Many-to-Many
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();


    }
}
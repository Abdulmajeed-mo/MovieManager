using MovieManager.Models;

namespace MovieManager.Services
{
    public class MovieService : IMovieService
    {

        public List<Movie> GetMovies()
        {

            Movie movie1 = new Movie
            {
                Id = 1,
                Name = "Interstellar",
                Year = 2014,
                Rating = 8.7m
            };

            Movie movie2 = new Movie
            {
                Id = 2,
                Name = "The Dark Knight",
                Year = 2008,
                Rating = 9.0m
            };

            Movie movie3 = new Movie
            {
                Id = 3,
                Name = "Inception",
                Year = 2010,
                Rating = 8.8m
            };

            Movie movie4 = new Movie
            {
                Id = 4,
                Name = "The Shawshank Redemption",
                Year = 1994,
                Rating = 9.3m
            };

            Movie movie5 = new Movie
            {
                 Id= 5,
                Name = "Parasite",
                Year = 2019,
                Rating = 8.5m
            };

            Movie movie6 = new Movie
            {
                Id = 6,
                Name = "Whiplash",
                Year = 2014,
                Rating = 8.5m
            };



            List<Movie> movies = new List<Movie>();

            movies.Add(movie1);
            movies.Add(movie2);
            movies.Add(movie3);
            movies.Add(movie4);
            movies.Add(movie5);
            movies.Add(movie6);

            return movies;
        }
      


    }
}
























////ذا اوبجكت  سويته من المودل عشان يجيب البيانات من المودل
////لأن أسماء المتغيرات في C# تبدأ بحرف صغير.
//Movie movie1 = new Movie
//{
//    Id = 1,
//    Name = "Interstellar",
//    Year = 2014,
//    Rating = 9,


//};

////اوبجكت ثاني طبعا الاوبجكت نفس خصائص الكلاس اللي سوناه بالمودل ولكن بقيم اخرى 
//Movie movie2 = new Movie
//{
//    Id = 2,
//    Name = "Obsession ",
//    Year = 2026,
//    Rating = 7.9m,

//};
//Movie movie3 = new Movie
//{
//    Id = 3,
//    Name = "Into the Wild ",
//    Year = 2007,
//    Rating = 8.1m,

//};
//Movie movie4 = new Movie
//{
//    Id = 4,
//    Name = "Spring, Summer, Fall, Winter... and Spring ",
//    Year = 2011,
//    Rating = 8.0m,

//};
//Movie movie5 = new Movie
//{
//    Id = 5,
//    Name = "Samsara ",
//    Year = 2011,
//    Rating = 8.2m,

//};
//Movie movie6 = new Movie
//{
//    Id = 6,
//    Name = "The Tree of Life ",
//    Year = 2011,
//    Rating = 6.8m,

//};




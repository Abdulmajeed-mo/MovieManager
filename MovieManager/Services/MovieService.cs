using MovieManager.Data;
using MovieManager.DTOs;
using MovieManager.Models;
using System.Text.Json;

namespace MovieManager.Services
{
    public class MovieService : IMovieService
    {
        //fields














        //private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _apiKey;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AppDbContext _Db;

        //Constructors
        //الكونفقريشن هو باراميتر يستقبل الـ الاوبجكت الذي أنشأه الـ الفريم وورك.
        public MovieService(IConfiguration configuration , IHttpClientFactory httpClientFactory, AppDbContext dbContext)
        {
            //نأخذ القيمة الموجودة داخال الكونفقريشن ونضعها داخل _كونفقريشن.
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _baseUrl = _configuration["ApiSettings:BaseUrl"];
            _apiKey = _configuration["ApiSettings:ApiKey"];
            _Db = dbContext;
        }








        //Methods
        public async Task<List<Movie>> GetMovies()
        {

            //أنشأت HttpClient
             HttpClient client = _httpClientFactory.CreateClient();
            //عنوان الموقع
            client.BaseAddress = new Uri(_baseUrl);


            //السطر ذا يسوي اربع اشياء:
            //يرسل HTTP GET Request.
            //    يضيف مفتاح الـ API داخل الرابط.
            //    ينتظر حتى يرد TMDB.
            //    يخزن الرد القادم من TMDB.
            HttpResponseMessage response = await client.GetAsync($"/3/movie/popular?api_key={_apiKey}");
            
            if (response.IsSuccessStatusCode )
            {
                                                      //لقراءة الـ JSON.
                string json = await response.Content.ReadAsStringAsync();
                Console.WriteLine(json);
                //convert the json string to a list of movies
                MovieApiResponse movieApiResponse =
                    JsonSerializer.Deserialize<MovieApiResponse>(json,new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                //return the list of movies
                return movieApiResponse.Results;
            }


            //if it fails to get the data from the API, return an empty list
            return new List<Movie>();
        }






                    // التاسك لانها لا ترجع قيمة، فقط تنفذ العملية.
        public async Task AddMovie (Movie movie)
        {

            await _Db.Movies.AddAsync(movie);
            await _Db.SaveChangesAsync();
        }


      
           

    }
}





















//Movie movie1 = new Movie
//{
//    Id = 1,
//    Name = "Interstellar",
//    Year = 2014,
//    Rating = 8.7m
//};

//Movie movie2 = new Movie
//{
//    Id = 2,
//    Name = "The Dark Knight",
//    Year = 2008,
//    Rating = 9.0m
//};

//Movie movie3 = new Movie
//{
//    Id = 3,
//    Name = "Inception",
//    Year = 2010,
//    Rating = 8.8m
//};

//Movie movie4 = new Movie
//{
//    Id = 4,
//    Name = "The Shawshank Redemption",
//    Year = 1994,
//    Rating = 9.3m
//};

//Movie movie5 = new Movie
//{
//    Id = 5,
//    Name = "Parasite",
//    Year = 2019,
//    Rating = 8.5m
//};

//Movie movie6 = new Movie
//{
//    Id = 6,
//    Name = "Whiplash",
//    Year = 2014,
//    Rating = 8.5m
//};



//List<Movie> movies = new List<Movie>();

//movies.Add(movie1);
//movies.Add(movie2);
//movies.Add(movie3);
//movies.Add(movie4);
//movies.Add(movie5);
//movies.Add(movie6);

//return movies;

//------------------------------------------------------------------------------

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




using MovieManager.Data;
using MovieManager.DTOs;
using MovieManager.Models;
using System.Text.Json;
using MovieManager.Repositories;
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
        private readonly IMovieRepository _movieRepository;

        //Constructors
        //الكونفقريشن هو باراميتر يستقبل الـ الاوبجكت الذي أنشأه الـ الفريم وورك.
        public MovieService(IConfiguration configuration , IHttpClientFactory httpClientFactory, IMovieRepository movieRepository)
        {
            //نأخذ القيمة الموجودة داخال الكونفقريشن ونضعها داخل _كونفقريشن.
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _baseUrl = _configuration["ApiSettings:BaseUrl"];
            _apiKey = _configuration["ApiSettings:ApiKey"];
            _movieRepository = movieRepository;
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
        //public async Task AddMovie (Movie movie)
        //{

        //    await _Db.Movies.AddAsync(movie);
        //    await _Db.SaveChangesAsync();
        //}


      
           

    }
}
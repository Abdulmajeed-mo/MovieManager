using MovieManager.Data;
using MovieManager.DTOs;
using MovieManager.Models;
using System.Text.Json;
using MovieManager.Repositories;
namespace MovieManager.Services
{
    public class MovieService : IMovieService
    {
      
        
        //private  //fields
        private readonly string _baseUrl;
        private readonly string _apiKey;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMovieRepository _movieRepository;
        private readonly ILogger<MovieService> _logger;

        //Constructors
        //الكونفقريشن هو باراميتر يستقبل الـ الاوبجكت الذي أنشأه الـ الفريم وورك.
        public MovieService(IConfiguration configuration , IHttpClientFactory httpClientFactory, IMovieRepository movieRepository , ILogger<MovieService> logger)
        {
            //نأخذ القيمة الموجودة داخال الكونفقريشن ونضعها داخل _كونفقريشن.
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _baseUrl = _configuration["ApiSettings:BaseUrl"];
            _apiKey = _configuration["ApiSettings:ApiKey"];
            _movieRepository = movieRepository;
            _logger = logger;
        }








        //Methods
        public async Task<List<Movie>> GetMovies()
        {  

            _logger.LogInformation("Fetching all movies");




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
        public async Task AddMovie(Movie movie)
        {
            try
            {
                _logger.LogInformation("Adding a new movie");
                await _movieRepository.AddAsync(movie);
                _logger.LogInformation("Movie Added  Successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a movie");
                
            }


          
        }

     



    }
}
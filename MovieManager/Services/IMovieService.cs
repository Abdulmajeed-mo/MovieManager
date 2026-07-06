using MovieManager.Models;

namespace MovieManager.Services
{

    //في الابستركشن. لازم تكون الفكره انك تفصل بين الـ الكنترولر والـ السيرفس. عشان لو غيرت طريقة جلب البيانات من قاعدة البيانات، ما أحتاج أغير الكود في الـ الكنترولر.
    
    //هذا هوالعقد.
    public interface IMovieService
    {
        List<Movie> GetMovies();
    } 

}

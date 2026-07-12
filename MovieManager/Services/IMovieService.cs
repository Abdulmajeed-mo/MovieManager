using MovieManager.Models;
using System.Threading.Tasks;
namespace MovieManager.Services
{

    //في الابستركشن. لازم تكون الفكره انك تفصل بين الـ الكنترولر والـ السيرفس. عشان لو غيرت طريقة جلب البيانات من قاعدة البيانات، ما أحتاج أغير الكود في الـ الكنترولر.
    
    //هذا هوالعقد.
    public interface IMovieService
    {
        Task<List<Movie>> GetMovies();
    } 

}

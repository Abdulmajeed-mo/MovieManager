using Microsoft.AspNetCore.Mvc;
using MovieManager.Models;

namespace MovieManager.Controllers
{
    public class MoviesController : Controller
    {
        public IActionResult Index()
        {
            //الفور ايتش هي عباره عن اللوب كل شوي تتحقق  بالبداية تكتب اسم الكلاس وبعدها تعطيه اسم موقت زي تشيك وبعدعا ان وبعدها تحط القائمه اللي سويتها انت اللي هي الموفيز

            //استدعاء الميثود شو موفيز وإرسال قائمة الأفلام لها حتى تتمكن من استخدامها داخلها.
            //لأننا في الاستدعاء لا نكتب النوع، نكتب المتغير فقط.
            List<Movie> movies = GetMovies();
            ShowMovies(movies);
            return View();

        }

        //فويد تعني أن الـ الميثود لا تُرجع قيمة, قيمه لليروتين).
        //الميثود تجمع الأوامر المتعلقة بوظيفة واحدة في مكان واحد، ثم أستدعيها وقت ما أحتاجهالأن وظيفتها عرض الأفلام
        //أن البيانات التي تستقبلها الميثود تكتب داخل()
        //داخل  تعريف الـ الميثود أكتب:النوع + اسم المتغير
        public void ShowMovies(List<Movie> allMovies)
        {
            //إذا كانت البيانات خارج الميثود، وأبي أستخدمها داخلها، لازم أرسلها للميثود عن طريق الـ الباراميتير.
            //استقبلت قائمة أفلام ثم استخدمت الفورايتش للمرور على كل فيلم داخل القائمة.
            //الميثود تستقبل لست موفي الللي هو الكلاس يصير كنوع واحد  ثم تتعامل مع كل الكلاس داخلها باستخدام فورايتش.
            foreach (Movie check in allMovies)
            {

                //في الفورايتش يكون عندي متغير مؤقت(مثل تشيك ) يمثل العنصر الحالي من القائمة.وللوصول إلى خصائص هذا العنصر أستخدم النقطة(.) مثل:
                if (check.Rating >= 9)
                {

                    //انت تبي تطبع الموفيز وعشان تطبعهم  تحط الاسم المؤقت اللي استخدمتع في الفورايتش وعشان توصل للخصائص الللي بالكلاس تحط نقطه وبعدها الخاصيه
                    Console.WriteLine("Excellent Movie");
                }

                else
                {
                    Console.WriteLine("Good Movie");
                }

            }


        }
        //أصبح الديتيلز افضل ليش؟ لأنه يتعامل مع حالة عدم وجود البيانات.
        public IActionResult Details(int id)
        {
            List<Movie> movies = GetMovies();

            Movie movie = movies.FirstOrDefault(x=> x.Id==id);

            if (movie==null)
            {
                return NotFound();
            }



            //return Content($" Movie Id: {movie.Id}\n " +
            //    $"Movie Name: {movie.Name}\n " +
            //    $"Movie Year: {movie.Year}\n " +
            //    $"Movie Rating: {movie.Rating} ");

            //ليش حطيت موفي داخل ال الريترين؟ عشان تنتقل البيانات ويقراها الفيو 
            return View(movie);

        }

  
        public List<Movie> GetMovies()
        {

            //ذا اوبجكت  سويته من المودل عشان يجيب البيانات من المودل
            //لأن أسماء المتغيرات في C# تبدأ بحرف صغير.
            Movie movie1 = new Movie
            {
                Id = 1,
                Name = "Interstellar",
                Year = 2014,
                Rating = 9,


            };

            //اوبجكت ثاني طبعا الاوبجكت نفس خصائص الكلاس اللي سوناه بالمودل ولكن بقيم اخرى 
            Movie movie2 = new Movie
            {
                Id = 2,
                Name = "Obsession ",
                Year = 2026,
                Rating = 7.9m,

            };
            Movie movie3 = new Movie
            {
                Id = 3,
                Name = "Into the Wild ",
                Year = 2007,
                Rating = 8.1m,

            };
            Movie movie4 = new Movie
            {
                Id = 4,
                Name = "Spring, Summer, Fall, Winter... and Spring ",
                Year = 2011,
                Rating = 8.0m,

            };
            Movie movie5 = new Movie
            {
                Id = 5,
                Name = "Samsara ",
                Year = 2011,
                Rating = 8.2m,

            };
            Movie movie6 = new Movie
            {
                Id = 6,
                Name = "The Tree of Life ",
                Year = 2011,
                Rating = 6.8m,

            };






            //list ننشئها داخل الاندكس وتحديدا بعد الاوبجكت وقبل الريتيرن 

            //تقريبا هي زي الاوبجكت  اول تحط لست وبعدها تاخذ نوع الكلاس الللي تبي تسوي فيه قائمه وبعدها تسسميه انت 
            //Add() لا تُكتب داخل اللست الـ List. يعني بدون اقواس
            //أولًا أنشئ القائمة، ثم أضف العناصر إليها باستخدام Add().
            List<Movie> movies = new List<Movie>();
            //Add() هي Method(دالة) وظيفتها: إضافة عنصر واحد إلى الـ List.
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


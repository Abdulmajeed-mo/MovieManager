using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using MovieManager.Models;
using MovieManager.Services;
using System.Reflection.Metadata;
using System.Xml.Linq;
namespace MovieManager.Controllers
{


//    ذا كان الكلاس أو الـ Interface في Namespace مختلف، فلا تستطيع استخدامه إلا إذا:

//أضفت using المناسب.
//أو كتبت الاسم الكامل.

//وهذه قاعدة ستتكرر كثيرًا في مشاريع ASP.NET Core.


    public class MoviesController : Controller
    {

        //لازم يكون منفصل عن التنفيذ

        //هذا هو Implementation

        //الكنترولر يعتمد على الـ الانترفيس، وليس على الـ كلاس نفسه وهذا ما يسمى الديبندنسي انجيكشن.

        //برايفت يعني هذا المتغير لا يستطيع أحد استخدامه إلا من داخل الكنترولر + لا يمكن أي كلاس آخر أن يصل إليه.
        private readonly IMovieService _service;
        //Constructor
        //السيرفس هو باراميتر يستقبل الـ الاوبجكت الذي أنشأه الـ الفريم وورك.
        public MoviesController(IMovieService service)
        {
            //نأخذ القيمة الموجودة داخل سرفيس ونضعها داخل _سيرفس.
            _service= service;
        }





        [HttpPost]
        public IActionResult TestValidation([FromBody] Movie movie)
        {
            if (ModelState.IsValid)
            {
                return Content("Movie Saved Successfully");
            }

            return Content("Invalid Data");

        }


        [HttpPost]
        public IActionResult AddMovie([FromBody]Movie movie)
        {
            if(ModelState.IsValid)
            {
                return Content("Movie Added  Successfully");

            }
            return Content("Invalid Data Info");

        }









        [HttpGet]
        public IActionResult TestGet()
        {
            Movie movie = new Movie
            {
                Id = 2,
                Name = "Obsession",
                Year = 2026,
                Rating = 7.9m
            };

            return Json(movie);
        }
        [HttpPost]
        public IActionResult Test([FromBody] Movie movie)
        {

            return Json(movie);
        }


        // نستقبل مجموعة أفلام مرة واحدة من الـ Request Body.يل
        [HttpPost]
        public IActionResult TestCollection([FromBody] List<Movie> movies)
        {
            return Json(movies);
        }

        [HttpGet]
        public IActionResult TestHeader([FromHeader] string  ApiKey  )
        {
            return Content($"Your Api Key is: {ApiKey}");
        }



            // استخدمت موفي الكلاس  موفي البندق لأن البيانات القادمة من المستخدم تتجمع داخل كائن واحد من نوع كلاس الموفي 
            // بدل ما أستقبل كل خاصية في باراميتر منفصل.
            //لازم يكون اثنين اكشن الاول يعرض الصفحة والثاني يستقبل البيانات
            //الاول
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                return Content("Movie Saved Successfully");
            }

            return Content("Invalid Data");

        }



        public IActionResult Index()
        {
            //الفور ايتش هي عباره عن اللوب كل شوي تتحقق  بالبداية تكتب اسم الكلاس وبعدها تعطيه اسم موقت زي تشيك وبعدعا ان وبعدها تحط القائمه اللي سويتها انت اللي هي الموفيز

            //استدعاء الميثود شو موفيز وإرسال قائمة الأفلام لها حتى تتمكن من استخدامها داخلها.
            //لأننا في الاستدعاء لا نكتب النوع، نكتب المتغير فقط.
            List<Movie> movies = _service.GetMovies();
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
            List<Movie> movies = _service.GetMovies();

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



        //عشان اجلب االفلم الواحد بالاي دي الخاص به
        [HttpGet]
        public IActionResult GetMovieById(int foundMovie)
        {
            List<Movie> movies = _service.GetMovies();
            //الفيلم الحالي في القائمة
            Movie movie = movies.FirstOrDefault(x => x.Id== foundMovie);
          
            if (movie == null)
            {
                return NotFound();
            }
            return Json(movie);
        
        }






     //       //عشان اجلب كل الافلام من داخل الللسته الللي تحت  وباستخدام بوست مان راح نظهر في ملف جيسون
     //       [HttpGet]
     //public IActionResult GetAllMovies () 
     //   {
     //       return Json (GetMovies()); 
     //   }








      




        //    //list ننشئها داخل الاندكس وتحديدا بعد الاوبجكت وقبل الريتيرن 

        //    //تقريبا هي زي الاوبجكت  اول تحط لست وبعدها تاخذ نوع الكلاس الللي تبي تسوي فيه قائمه وبعدها تسسميه انت 
        //    //Add() لا تُكتب داخل اللست الـ List. يعني بدون اقواس
        //    //أولًا أنشئ القائمة، ثم أضف العناصر إليها باستخدام Add().
        //    List<Movie> movies = new List<Movie>();
        //    //Add() هي Method(دالة) وظيفتها: إضافة عنصر واحد إلى الـ List.
        //    movies.Add(movie1);
        //    movies.Add(movie2);
        //    movies.Add(movie3);
        //    movies.Add(movie4);
        //    movies.Add(movie5);
        //    movies.Add(movie6);


        //    return movies;

        //}

    }
}


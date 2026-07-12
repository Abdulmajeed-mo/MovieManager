//Configuration
using MovieManager.Middlewere;
using MovieManager.Services;
using static MovieManager.Controllers.MoviesController;
using static System.Net.Mime.MediaTypeNames;
using MovieManager.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

builder.Services.AddScoped<IMovieService, MovieService>();

// Add services to the container.
builder.Services.AddControllersWithViews(); 

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

//middlewere 
//context = يمثل الطلب الحالي.
//next = ينقل الطلب إلى الـ  ميدل وير أو الـ الراوت الذي بعده.
//داخل MapGet أو Use لا أكتب أنواع البارامترات، لأن C# يستنتجها تلقائيًا.
//Middleware يستطيع تنفيذ كود قبل الطلب وبعده بعد رجوع الاستجابة، ولذلك يُسمى Pipeline.
//app.Use(async (context, next) => 
//{


//    //أي طلب يخص مشروع ادارة الافلام سيتم تسجيل بداية الطلب ونهايته، وهذا مفيد جدًا أثناء التطوير وتتبع الأخطاء.
//    Console.WriteLine("Movie Request Started");

//    await next();

//    Console.WriteLine("Movie Request Finished");


//});
// وظيفتها تسجيل الـ Custom Middleware داخل الـ Pipeline للتطبيق.

//middlewere 1
app.UseMiddleware<LogMiddleware>();

//middlewere 2
app.UseWhen(
    context=>
     context.Request.Path.StartsWithSegments("/movies") ||
    context.Request.Path.StartsWithSegments("/movie/"),
    appBuilder =>
    {
        appBuilder.UseMiddleware<MovieValidationMiddleware>();
    } );



//Route
//الماب قت () تكتب داخل البروقرام سي اس  لأنها مسؤولة عن استقبال طلبات القت وربطها بمسار الراوت
//() => تعني: نفّذ الكود عند استدعاء الـ Route.
//app.MapGet("/movies",()=> "Welcome to Movie Manager");
////QueryString
//app.MapGet("/search",(string ?name)=> $"Searching for: {name} ");
////Route Parameter
////? يأتي بعد اسم الـ Route Parameter وليس قبله.

////app.MapGet("/movie/{id?}", (int? id )=> id == null ? $"Movie Id not provided" :  $"Movie Id:{id}");
////Route Constrain
//app.MapGet("/movie/{id:int}", (int id) => $"Movie Id:{id}");





app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
//سيقوم باكتشاف جميع الكنترولر في المشروع كامل مع ميثود اكشن
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

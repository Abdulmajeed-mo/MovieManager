//Configuration
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}






//الماب قت () تكتب داخل البروقرام سي اس  لأنها مسؤولة عن استقبال طلبات القت وربطها بمسار الراوت
//() => تعني: نفّذ الكود عند استدعاء الـ Route.
app.MapGet("/movies",()=> "Welcome to Movie Manager");
//QueryString
app.MapGet("/search",(string name)=> $"Searching for: {name} ");
//Route Parameter
//? يأتي بعد اسم الـ Route Parameter وليس قبله.

//app.MapGet("/movie/{id?}", (int? id )=> id == null ? $"Movie Id not provided" :  $"Movie Id:{id}");

app.MapGet("/movie/{id:int}", (int id) => $"Movie Id:{id}");





app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

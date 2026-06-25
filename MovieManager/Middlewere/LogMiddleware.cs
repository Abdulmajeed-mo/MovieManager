namespace MovieManager.Middlewere
{
    public class LogMiddleware
    {

        //استقبلت RequestDelegate من ASP.NET Core وحفظته داخل المتغير _next حتى أستخدمه لاحقًا لتمرير الطلب إلى المرحلة التالية.
      private RequestDelegate  _next;


        //الـ Constructor اسمه نفس اسم الكلاس.
               public LogMiddleware (RequestDelegate next)
        {
            _next = next;
        }


        //داخل الكلاس لا نكتب Lambda.
        //InvokeAsync هي أول Method يستدعيها ASP.NET Core عند وصول أي Request إلى هذا الـ Middleware.
        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("Movie Request Started");
            await _next(context);
            Console.WriteLine("Movie Request Finished");

        }







    }
}

namespace MovieManager.Middlewere
{
    public class MovieValidationMiddleware
    {
        // 1. البداية: تعريف الكلاس باسم الميدل وير الخاص بك.
        // 2. وبعدها: إنشاء متغير برايفت لحفظ الخطوة التالية في البايبلاين.
        // 3. الباني "الكونستركتر": إنشاء الكونستركتور وبنفس اسم الكلاس لاستلام الخطوة التالية وتخزينها.
        // 4. تسوي ميثود: إنشاء ميثود التشغيل الأساسية (إنفوك أسينك) لاستقبال الطلب وتمريره.      
        private RequestDelegate _next;


            public MovieValidationMiddleware (RequestDelegate next)
        {

            _next = next;
        }

        public async Task InvokeAsync( HttpContext context)
        {

            Console.WriteLine("Movie Validation Started");
            await _next(context);

            Console.WriteLine(" Movie Validation Finished");

        }



    }
}

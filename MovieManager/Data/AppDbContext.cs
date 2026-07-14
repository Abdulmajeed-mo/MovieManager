

using Microsoft.EntityFrameworkCore;

namespace MovieManager.Data
{

    //جعلنا ApplicationDbContext             يرث من DbContext  .

    //DbContext
    //و الكلاس المسؤول عن الاتصال بقاعدة البيانات وإدارتها داخل الانتيتي فريمورك
    //من خلاله يتم تنفيذ جميع عمليات قاعدة البيانات مثل القراءة والإضافة والتعديل والحذف، عمليات "الكرد"د

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}

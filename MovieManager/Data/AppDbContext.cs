

using Microsoft.EntityFrameworkCore;
using MovieManager.Models;

namespace MovieManager.Data
{

    //جعلنا AppDbContext             يرث من DbContext  .

    //DbContext
    //و الكلاس المسؤول عن الاتصال بقاعدة البيانات وإدارتها داخل الانتيتي فريمورك
    //من خلاله يتم تنفيذ جميع عمليات قاعدة البيانات مثل القراءة والإضافة والتعديل والحذف، عمليات "الكرد"د

    public class AppDbContext : DbContext
    {
        //يمثل جدولًا في قاعدة البيانات، وكل داتا بيس سيت  يقابل جدولًا واحدًا.
        //عندي جدول اسمه موفييز، وكل صف فيه يمثل كائنًا من نوع موفي.

        public DbSet<Movie> Movies { get; set; }
        //أنشئ لي جدولًا جديدًا اسمه Reviews
        public DbSet<Review> Reviews { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}

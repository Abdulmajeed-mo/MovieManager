using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MovieManager.Models
{
    public class Review
    {

        public int Id { get; set; }
        public string Comment { get; set; }


        //Foreign Key 
        public int MovieId { get; set; }

        //Navigation Property
        //تسمح لك بالوصول إلى بيانات الفيلم من داخل المراجعة.
        public Movie Movie { get; set; }
    }
}

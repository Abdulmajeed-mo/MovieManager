
namespace MJDVerse.Domain.Entities
{
    public class Review
    {

        public int Id { get; set; }
        public string Comment { get; set; } = string.Empty;

        //Foreign Key 
        public int MovieId { get; set; }

        public string UserId { get; set; } = string.Empty;

        //Navigation Property
        //تسمح لك بالوصول إلى بيانات الفيلم من داخل المراجعة.
        public Movie Movie { get; set; } = null!;


    }
}


using System.Text.Json.Serialization;
namespace MovieManager.Models
{
    public class Movie
    {


        //تعتبر خصائص وهنا تكتب في المودل
        //تمنع المستخدم من إرسال قيمة هذه الخاصية 
        //هو يشتغل في  Model Binding
        public int Id { get; set; }
      
        [JsonPropertyName("title")]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; }

        public int Duration { get; set; }

        [JsonPropertyName("vote_average")]
         public decimal Rating { get; set; }
        
        public string PosterUrl { get; set; }


        //ولذلك EF Core يستخدمها كثيرًا في العلاقات One-to-Many.

        //تسمح لك بالوصول إلى جميع مراجعات الفيلم
        public ICollection<Review> Reviews { get; set; }


    }
}

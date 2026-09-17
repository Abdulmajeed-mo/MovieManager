

namespace MJDVerse.Domain.Entities
{
    public class Movie
    {


        //تعتبر خصائص وهنا تكتب في المودل
        //تمنع المستخدم من إرسال قيمة هذه الخاصية 
        //هو يشتغل في  Model Binding
        public int Id { get; set; }

        public int TmdbId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public ICollection<CastMember> CastMembers { get; set; } = new List<CastMember>();

        public ICollection<CrewMember> CrewMembers { get; set; } = new List<CrewMember>();
        public ICollection<WatchlistItem> WatchlistItems { get; set; } = new List<WatchlistItem>();

        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();

        public ICollection<WatchHistory> WatchHistories { get; set; } = new List<WatchHistory>();

        public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();

        public DateTime ReleaseDate { get; set; }
        public int RuntimeMinutes { get; set; }

        public decimal AverageRating { get; set; }
        public string? PosterUrl { get; set; }

        //ولذلك EF Core يستخدمها كثيرًا في العلاقات One-to-Many.

        //تسمح لك بالوصول إلى جميع مراجعات الفيلم
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

    }
}

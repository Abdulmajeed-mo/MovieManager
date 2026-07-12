using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Razor.Hosting;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MovieManager.Validations;
namespace MovieManager.Models
{
    public class Movie
    {

        //تعتبر خصائص وهنا تكتب في المودل
        //تمنع المستخدم من إرسال قيمة هذه الخاصية 
        //هو يشتغل في  Model Binding
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; }

        [JsonPropertyName("vote_average")]
        public decimal Rating { get; set; }

    }
}

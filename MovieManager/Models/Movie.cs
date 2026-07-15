using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Razor.Hosting;
using Microsoft.EntityFrameworkCore;
using MovieManager.Validations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace MovieManager.Models
{
    public class Movie
    {
        //يمثل جدولًا في قاعدة البيانات، وكل داتا بيس سيت  يقابل جدولًا واحدًا.
        //عندي جدول اسمه موفييز، وكل صف فيه يمثل كائنًا من نوع موفي.
        public DbSet<Movie> Movies { get; set; }


        //تعتبر خصائص وهنا تكتب في المودل
        //تمنع المستخدم من إرسال قيمة هذه الخاصية 
        //هو يشتغل في  Model Binding
        public string Director { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; }

        [JsonPropertyName("vote_average")]
        public decimal Rating { get; set; }

    }
}

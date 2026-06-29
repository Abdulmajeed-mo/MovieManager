using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Razor.Hosting;
using System.ComponentModel.DataAnnotations;

namespace MovieManager.Models
{
    public class Movie
    {
        //تعتبر خصائص وهنا تكتب في المودل
        //تمنع المستخدم من إرسال قيمة هذه الخاصية 
        //هو يشتغل في  Model Binding
        [BindNever]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Range(1990,2030)]
        public int Year { get; set; }
        [Range(0,10)]
        public decimal Rating { get; set; }
        
    }
}

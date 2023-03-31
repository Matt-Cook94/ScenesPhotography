using System.ComponentModel.DataAnnotations;
using ScenesPhotography.Web.Data.Enum;

namespace ScenesPhotography.Web.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Body is required")]
        public string Body { get; set; }
        public BlogCategory? BlogCategory { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [DataType(DataType.Date)]
        public DateTime? UpdatedAt { get; set; }
        public string? Image { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}

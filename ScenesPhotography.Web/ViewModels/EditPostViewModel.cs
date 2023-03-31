using ScenesPhotography.Web.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace ScenesPhotography.Web.ViewModels
{
    public class EditPostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public BlogCategory? BlogCategory { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime UpdatedAt { get; set; }
        public string? Image { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ScenesPhotography.Web.Models;

namespace ScenesPhotography.Web.ViewModels
{
    public class CreateCommentViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Body { get; set; }

        [ForeignKey("Post")]
        public int PostId { get; set; }
    }
}

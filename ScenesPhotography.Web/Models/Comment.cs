using System.ComponentModel.DataAnnotations.Schema;

namespace ScenesPhotography.Web.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public DateTime PostedAt { get; set; }

        [ForeignKey("Post")]
        public int PostId { get; set; }
    }
}

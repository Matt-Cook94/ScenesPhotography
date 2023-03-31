using ScenesPhotography.Web.Models;

namespace ScenesPhotography.Web.ViewModels
{
    public class IndexViewModel
    {
        public int PageNumber { get; set; }
        public int PageCount { get; set; }
        public bool NextPage { get; set; }
        public IEnumerable<Post> Posts {get; set; }
        public IEnumerable<int> Pages { get; set; }
    }
}

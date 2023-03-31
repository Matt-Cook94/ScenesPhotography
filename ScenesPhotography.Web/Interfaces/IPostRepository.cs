using ScenesPhotography.Web.Data.Enum;
using ScenesPhotography.Web.Models;
using ScenesPhotography.Web.ViewModels;

namespace ScenesPhotography.Web.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAll();
        Task<IndexViewModel> GetPagePosts(int pageNumber);
        Task<Post> GetByIdAsync(int id);
        Task<Post> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Post>> GetAllPostsByCategory(BlogCategory blogCategory);
        bool Add(Post post);
        bool Update(Post post);
        bool Delete(Post post);
        bool Save();
    }
}

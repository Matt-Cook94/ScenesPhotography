using ScenesPhotography.Web.Data;
using ScenesPhotography.Web.Data.Enum;
using ScenesPhotography.Web.Helpers;
using ScenesPhotography.Web.Interfaces;
using ScenesPhotography.Web.Models;
using ScenesPhotography.Web.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ScenesPhotography.Web.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Post post)
        {
            _context.Add(post);
            return Save();
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<IndexViewModel> GetPagePosts(int pageNumber)
        {
            var pageSize = 4;
            var skipAmount = pageSize * (pageNumber - 1);
            var postsCount = _context.Posts.Count();
            var pageCount = (int)Math.Ceiling((double)postsCount / pageSize);

            return new IndexViewModel()
            {
                PageNumber = pageNumber,
                PageCount = pageCount,
                NextPage = postsCount > skipAmount + pageSize,
                Pages = PageHelper.PageNumbers(pageNumber, pageCount).ToList(),
                Posts = await _context.Posts.Skip(skipAmount).Take(pageSize).ToListAsync()
            };
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            return await _context.Posts.Include(c => c.Comments).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Post> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Posts.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public bool Update(Post post)
        {
            _context.Update(post);
            return Save();
        }

        public bool Delete(Post post)
        {
            _context.Remove(post);
            return Save();
        }

        public bool Save()
        {
            var result = _context.SaveChanges();
            return result > 0;
        }

        public async Task<IEnumerable<Post>> GetAllPostsByCategory(BlogCategory blogCategory)
        {
            return await _context.Posts.Where(p => p.BlogCategory == blogCategory).ToListAsync();
        }
    }
}

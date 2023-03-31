using ScenesPhotography.Web.Data;
using ScenesPhotography.Web.Interfaces;
using ScenesPhotography.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace ScenesPhotography.Web.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Delete(Comment comment)
        {
            _context.Remove(comment);
            return Save();
        }

        public bool Save()
        {
            var result = _context.SaveChanges();
            return result > 0;
        }
    }
}

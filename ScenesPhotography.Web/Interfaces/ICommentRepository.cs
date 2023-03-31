using ScenesPhotography.Web.Models;

namespace ScenesPhotography.Web.Interfaces
{
    public interface ICommentRepository
    {
        bool Delete(Comment comment);
        bool Save();
    }
}

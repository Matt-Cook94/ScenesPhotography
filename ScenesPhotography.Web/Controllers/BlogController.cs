using ScenesPhotography.Web.Data;
using ScenesPhotography.Web.Data.Enum;
using ScenesPhotography.Web.Interfaces;
using ScenesPhotography.Web.Models;
using ScenesPhotography.Web.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;

namespace ScenesPhotography.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;

        public BlogController(IPostRepository postRepository, ICommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
        }

        public async Task<IActionResult> Index(int pageNumber)
        {
            if (pageNumber < 1)
                return RedirectToAction("Index", new { pageNumber = 1 });

            var indexViewModel = await _postRepository.GetPagePosts(pageNumber);

            return View(indexViewModel);
        }

        //[HttpGet] by default
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult Create(CreatePostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                var post = new Post()
                {
                    Title = postViewModel.Title,
                    Author = postViewModel.Author,
                    Description = postViewModel.Description,
                    Body = postViewModel.Body,
                    BlogCategory = postViewModel.BlogCategory,
                    CreatedAt = DateTime.Now,
                    Image = postViewModel.Image,
                };

                _postRepository.Add(post);
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Post upload failed.");
            return View(postViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Comment(CreateCommentViewModel commentViewModel)
        {
            if (ModelState.IsValid)
            {
                var post = await _postRepository.GetByIdAsync(commentViewModel.PostId);

                post.Comments.Add(new Comment()
                {
                    Name = commentViewModel.Name,
                    Body = commentViewModel.Body,
                    PostedAt = DateTime.Now,
                });

                _postRepository.Update(post);

                return RedirectToAction("Detail", new { id = commentViewModel.PostId });
            }

            ModelState.AddModelError("", "Comment upload failed.");
            return RedirectToAction("Detail", new { id = commentViewModel.PostId });
        }

        public async Task<IActionResult> Detail(int id)
        {
            Post post = await _postRepository.GetByIdAsync(id);
            return View(post);
        }

        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Edit(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);

            if (post == null)
            {
                return View("Error");
            }

            var postViewModel = new EditPostViewModel()
            {
                Title = post.Title,
                Author = post.Author,
                Description = post.Description,
                Body = post.Body,
                BlogCategory = post.BlogCategory,
                Image = post.Image
            };

            return View(postViewModel);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Edit(int id, EditPostViewModel postViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit post");
                return View("Edit", postViewModel);
            }

            var userPost = await _postRepository.GetByIdAsyncNoTracking(id);

            if (userPost != null)
            {
                var post = new Post()
                {
                    Id = id,
                    Title = postViewModel.Title,
                    Author = postViewModel.Author,
                    Description = postViewModel.Description,
                    Body = postViewModel.Body,
                    CreatedAt = userPost.CreatedAt,
                    UpdatedAt = DateTime.Now,
                    BlogCategory = postViewModel.BlogCategory,
                    Image = postViewModel.Image
                };

                _postRepository.Update(post);

                return RedirectToAction("Index");
            }

            return View(postViewModel);
        }

        // GET: BlogController/Category/blogCategory
        [Route("/Blog/Category/{BlogCategory}")]
        public async Task<ActionResult> Category(BlogCategory blogCategory)
        {
            var posts = await _postRepository.GetAllPostsByCategory(blogCategory);
            return View(posts);
        }

        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            var postDetails = await _postRepository.GetByIdAsync(id);

            if (postDetails == null) return View("Error");

            return View(postDetails);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);

            if (post == null) return View("Error");
            
            _postRepository.Delete(post);

            return RedirectToAction("Index");
        }
    }
}

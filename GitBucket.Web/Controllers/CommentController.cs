using GitBucket.Data.Repositories.Interfaces;
using GitBucket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GitBucket.Web.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommentController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public IActionResult GetAll(string prId)
        {
            var comments = _unitOfWork.CommentRepository.GetAll().Where(c => c.PullRequestId == prId);

            return Json(comments);
        }

        [HttpPost]
        public IActionResult Create(string content, string prId)
        {
            var comment = new Comment()
            {
                Id = Guid.NewGuid().ToString(),
                Title = content,
                PullRequestId = prId,
                UserId = User.Claims.First().Value
            };

            _unitOfWork.CommentRepository.Add(comment);
            _unitOfWork.Save();

            return Json(comment);
        }

        public IActionResult Delete(string id, string prId)
        {

            var commentFromDb = this._unitOfWork.CommentRepository.GetFirstOrDefault(r => r.Id == id);
            if (commentFromDb == null)
            {
                return NotFound();
            }

            _unitOfWork.CommentRepository.Remove(commentFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Comment deleted successfully!";
            return RedirectToAction("Details", "PullRequest", new { id = prId });
        }
    }
}

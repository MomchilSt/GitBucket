using GitBucket.Data.Repositories.Interfaces;
using GitBucket.Models;
using Microsoft.AspNetCore.Mvc;

namespace GitBucket.Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommentController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public IActionResult GetAll(int prId)
        {

            var comments = _unitOfWork.CommentRepository.GetAll().Where(c => c.PullRequestId == prId);

            return Json(comments);
        }

        [HttpPost]
        public IActionResult Create(string content, int prId)
        {
            var comment = new Comment()
            {
                Title = content,
                PullRequestId = prId,
                UserId = 1
            };

            _unitOfWork.CommentRepository.Add(comment);
            _unitOfWork.Save();

            return Json(comment);
        }

        public IActionResult Delete(int id, int prId)
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

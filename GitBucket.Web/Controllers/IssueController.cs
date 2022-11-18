using GitBucket.Data.Repositories.Interfaces;
using GitBucket.Models;
using GitBucket.Models.InputModels;
using GitBucket.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GitBucket.Web.Controllers
{
    public class IssueController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public IssueController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public IActionResult Index(int? id)
        {
            var issues = _unitOfWork.IssueRepository.GetAll().Where(r => r.RepositoryId == id);

            var model = new IssueViewModel{ RepoId = id, Issues= issues };

            return View(model);
        }

        public IActionResult Create(int id)
        {
            var model = new IssueInputViewModel { RepositoryId = id };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IssueInputViewModel model)
        {
            if (model.Title == model.Content)
            {
                ModelState.AddModelError("title", "Title and content cannot be equal!");
            }

            if (ModelState.IsValid)
            {
                var repoFromDb = this._unitOfWork.RepoRepository.GetFirstOrDefault(r => r.Id == model.RepositoryId);

                var issue = new Issue()
                {
                    Title = model.Title,
                    Content = model.Content,
                    RepositoryId = model.RepositoryId,
                    UserId = 1
                };

                _unitOfWork.IssueRepository.Add(issue);
                _unitOfWork.Save();

                TempData["success"] = "Issue created successfully!";
                return RedirectToAction("Index", new { id = model.RepositoryId });
            }
            return this.RedirectToAction("Create", new { id = model.RepositoryId });
        }

        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var issueFromDb = _unitOfWork.IssueRepository.GetFirstOrDefault(r => r.Id == id);
            if (issueFromDb == null)
            {
                return NotFound();
            }

            var repoName = _unitOfWork.RepoRepository.GetFirstOrDefault(r => r.Id == issueFromDb.RepositoryId).Name;

            var model = new IssueUpdateViewModel
            {
                Title = issueFromDb.Title,
                Content = issueFromDb.Content,
                RepoName = repoName,
                RepoId = issueFromDb.RepositoryId,
                Id = issueFromDb.Id
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(IssueUpdateViewModel model)
        {
            if (model.Title == model.Content)
            {
                ModelState.AddModelError("title", "Title and content cannot be equal!");
            }

            if (ModelState.IsValid)
            {
                var issueFromDb = this._unitOfWork.IssueRepository.GetFirstOrDefault(r => r.Id == model.Id);

                issueFromDb.Title = model.Title;
                issueFromDb.Content = model.Content;

                _unitOfWork.IssueRepository.Update(issueFromDb);
                _unitOfWork.Save();
                TempData["success"] = "Issue update successfully!";
                return RedirectToAction("Index", new { id = issueFromDb.RepositoryId});
            }
            return this.View(model);
        }

        public IActionResult Delete(int? id, int? repoId)
        {
            var issueeFromDb = this._unitOfWork.IssueRepository.GetFirstOrDefault(r => r.Id == id);
            if (issueeFromDb == null)
            {
                return NotFound();
            }

            _unitOfWork.IssueRepository.Remove(issueeFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Issue deleted successfully!";
            return RedirectToAction("Index", new { id = repoId });
        }
    }
}

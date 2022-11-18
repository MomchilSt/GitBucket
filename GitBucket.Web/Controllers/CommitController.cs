using GitBucket.Data.Repositories.Interfaces;
using GitBucket.Models;
using GitBucket.Models.InputModels;
using GitBucket.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GitBucket.Web.Controllers
{
    public class CommitController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommitController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public IActionResult Commit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var repoFromDb = _unitOfWork.RepoRepository.GetFirstOrDefault(r => r.Id == id);
            if (repoFromDb == null)
            {
                return NotFound();
            }

            var model = new CommitInputViewmodel
            {
                RepoId = repoFromDb.Id,
                RepoName = repoFromDb.Name,
                RepoContent = repoFromDb.Content
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Commit(CommitInputViewmodel model)
        {
            if (model.Title == model.RepoContent)
            {
                ModelState.AddModelError("title", "Title and content cannot be equal!");
            }

            if (ModelState.IsValid)
            {
                var repoFromDb = this._unitOfWork.RepoRepository.GetFirstOrDefault(r => r.Id == model.RepoId);
                var oldContent = repoFromDb.Content;

                repoFromDb.Content = model.RepoContent;
                _unitOfWork.RepoRepository.Update(repoFromDb);

                var commit = new Commit()
                {
                    Title = model.Title,
                    Content = model.RepoContent,
                    RepositoryId = model.RepoId,
                    Repository = repoFromDb,
                    ContentBeforeCommit = oldContent,
                    UserId = 1
                };
                _unitOfWork.CommitRepository.Add(commit);
                repoFromDb.Commits.Add(commit);
                _unitOfWork.Save();

                TempData["success"] = "Commited successfully!";
                return RedirectToAction("Details","Repository", new { id = model.RepoId});
            }
            return this.RedirectToAction("Commit", new { id = model.RepoId });
        }

        public IActionResult History(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var model = new CommitsHistoryViewModel 
            { 
                RepoId = id,
                CommitsHistory = _unitOfWork.CommitRepository.GetAll().Where(x => x.RepositoryId == id) 
            };

            return View(model);
        }

        public IActionResult Delete(int? id, int? repoId)
        {
            var commitFromDb = this._unitOfWork.CommitRepository.GetFirstOrDefault(r => r.Id == id);
            if (commitFromDb == null)
            {
                return NotFound();
            }
            var repo = _unitOfWork
                .RepoRepository
                .GetFirstOrDefault(x => x.Id == repoId).Content = commitFromDb.ContentBeforeCommit;

            _unitOfWork.CommitRepository.Remove(commitFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Commit deleted successfully!";
            return RedirectToAction("History", new { id = repoId });
        }


        public IActionResult Update(int? id, int repoId)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var commitFromDb = _unitOfWork.CommitRepository.GetFirstOrDefault(r => r.Id == id);
            if (commitFromDb == null)
            {
                return NotFound();
            }

            var model = new CommitInputViewmodel
            {
                Title = commitFromDb.Title,
                RepoContent = commitFromDb.Content,
                CommitId = id,
                RepoId = repoId
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(CommitInputViewmodel model)
        {
            if (model.Title == model.RepoContent)
            {
                ModelState.AddModelError("name", "Name and content cannot be equal!");
            }

            if (ModelState.IsValid)
            {
                var repoFromDb = this._unitOfWork.RepoRepository.GetFirstOrDefault(r => r.Id == model.RepoId);
                var commitFromDb = this._unitOfWork.CommitRepository.GetFirstOrDefault(c => c.Id == model.CommitId);

                repoFromDb.Content = model.RepoContent;
                commitFromDb.Content = model.RepoContent;
                commitFromDb.Title = model.Title;

                _unitOfWork.RepoRepository.Update(repoFromDb);
                _unitOfWork.CommitRepository.Update(commitFromDb);

                _unitOfWork.Save();
                TempData["success"] = "Repository edited successfully!";
                return RedirectToAction("History", new { id = model.RepoId });
            }
            return RedirectToAction("Update", new { id = model.CommitId, repoId = model.RepoId });
        }
    }
}

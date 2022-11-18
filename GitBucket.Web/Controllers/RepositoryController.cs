using GitBucket.Data;
using GitBucket.Data.Repositories.Interfaces;
using GitBucket.Models;
using GitBucket.Models.InputModels;
using GitBucket.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GitBucket.Web.Controllers
{
    public class RepositoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public RepositoryController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var model = new RepositoriesViewModel 
            { 
                Repositories = _unitOfWork.RepoRepository.GetAll() ,
                Commits = _unitOfWork.CommitRepository.GetAll()
            };
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RepositoryCreateInputModel model)
        {
            if (model.Name == model.Content)
            {
                ModelState.AddModelError("name", "Name and content cannot be equal!");
            }

            if (ModelState.IsValid)
            {
                var repo = new Repository
                {
                    Access = model.Access,
                    Name = model.Name,
                    Content = model.Content
                };

                _unitOfWork.RepoRepository.Add(repo);
                _unitOfWork.Save();
                TempData["success"] = "Repository created successfully!";
                return RedirectToAction("Index");
            }
            return this.View(model);
        }

        public IActionResult Details(int? id)
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

            var model = new RepositoryDetailsViewModel
            {
                Id = repoFromDb.Id,
                Access = repoFromDb.Access,
                Name = repoFromDb.Name,
                Content = repoFromDb.Content,
                Commits = repoFromDb.Commits,
                PullRequests = repoFromDb.PullRequests,
                Issues = repoFromDb.Issues,
                CreatedDateTime= repoFromDb.CreatedDateTime
            };

            return View(model);
        }

        public IActionResult Edit(int? id)
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

            var model = new RepositoryEditInputModel 
            {
                Id = repoFromDb.Id,
                Access = repoFromDb.Access,
                Name = repoFromDb.Name,
                Content = repoFromDb.Content
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RepositoryEditInputModel model)
        {
            if (model.Name == model.Content)
            {
                ModelState.AddModelError("name", "Name and content cannot be equal!");
            }

            if (ModelState.IsValid)
            {
                var repoFromDb = this._unitOfWork.RepoRepository.GetFirstOrDefault(r => r.Id == model.Id);

                repoFromDb.Access = model.Access;
                repoFromDb.Name = model.Name;
                repoFromDb.Content = model.Content;

                _unitOfWork.RepoRepository.Update(repoFromDb);
                _unitOfWork.Save();
                TempData["success"] = "Repository edited successfully!";
                return RedirectToAction("Index");
            }
            return this.View(model);
        }

        public IActionResult Delete(int? id)
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

            var model = new RepositoryDeleteViewModel
            {
                Id = repoFromDb.Id,
                Access = repoFromDb.Access,
                Name = repoFromDb.Name,
                Content = repoFromDb.Content
            };

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var repoFromDb = this._unitOfWork.RepoRepository.GetFirstOrDefault(r => r.Id == id);
            if (repoFromDb == null)
            {
                return NotFound();
            }

            _unitOfWork.RepoRepository.Remove(repoFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Repository deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
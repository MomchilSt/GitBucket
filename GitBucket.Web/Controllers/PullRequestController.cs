using GitBucket.Data.Repositories;
using GitBucket.Data.Repositories.Interfaces;
using GitBucket.Models;
using GitBucket.Models.InputModels;
using GitBucket.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GitBucket.Web.Controllers
{
    [Authorize]
    public class PullRequestController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PullRequestController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public IActionResult Index(string? id)
        {
            var pullRequests = _unitOfWork.PullRequestRepository.GetAll().Where(x => x.TargetRepository == id);
            var repos = _unitOfWork.RepoRepository.GetAll();

            var model = new PullRequestsViewModel
            {
                PullRequests = pullRequests,
                Repositories = repos,
                RepoId = id,
                LoggedUserId = User.Claims.First().Value
            };

            return View(model);
        }

        public IActionResult Details(string? id)
        {
            var pullRequestFromDb = _unitOfWork.PullRequestRepository.GetFirstOrDefault(x => x.Id == id);
            var repos = _unitOfWork.RepoRepository.GetAll();

            var source = repos.FirstOrDefault(r => r.Id == pullRequestFromDb.Source);
            var target = repos.FirstOrDefault(r => r.Id == pullRequestFromDb.TargetRepository);
            var comments = _unitOfWork.CommentRepository.GetAll().Where(c => c.PullRequestId == id);

            var commentsModel = new CommentsViewModel
            {
                PrId = pullRequestFromDb.Id,
                TargetRepo = pullRequestFromDb.TargetRepository,
                Comments = comments,
                Repositories = repos,
                LoggedUserId = User.Claims.First().Value
            };

            var model = new PullRequestViewModel
            {
                Id = pullRequestFromDb.Id,
                Name = pullRequestFromDb.Name,
                TargetRepository = pullRequestFromDb.TargetRepository,
                Source = pullRequestFromDb.Source,
                SourceName = source.Name,
                SourceContent = source.Content,
                TargetRepositoryName = target.Name,
                TargetRepositoryContent =  target.Content,
                Comments = commentsModel,
                LoggedUserId = User.Claims.First().Value
            };

            return View(model);
        }

        public IActionResult Create(string? id)
        {
            var source = _unitOfWork.RepoRepository.GetFirstOrDefault(x => x.Id == id);

            if (source == null)
            {
                return NotFound();
            }

            var model = new PullRequestInputViewModel
            {
                SourceName = source.Name,
                Repositories = _unitOfWork.RepoRepository.GetAll(),
                RepoId = id
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OpenPR(PullRequestInputViewModel model)
        {
            if (ModelState.IsValid)
            {
                var pullRequest = new PullRequest
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = model.Name,
                    Source = model.RepoId,
                    TargetRepository = model.TargetedRepo,
                    UserId = User.Claims.First().Value
                };

                _unitOfWork.PullRequestRepository.Add(pullRequest);
                _unitOfWork.Save();

                TempData["success"] = "Pull Request created successfully!";
                return RedirectToAction("Index", new { id = model.TargetedRepo });
            }

            return RedirectToAction("Create", new { id = model.RepoId});
        }

        public IActionResult Delete(string? prId, string id)
        {
            var prFromDb = this._unitOfWork.PullRequestRepository.GetFirstOrDefault(r => r.Id == prId);
            if (prFromDb == null)
            {
                return NotFound();
            }

            _unitOfWork.PullRequestRepository.Remove(prFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Pull Request deleted successfully!";
            return RedirectToAction("Index", new { id = id });
        }

        public IActionResult Approve(string? target, string? source, string? prId)
        {
            var targetRepoFromDb = this._unitOfWork.RepoRepository.GetFirstOrDefault(r => r.Id == target);
            var sourceRepoFromDb = this._unitOfWork.RepoRepository.GetFirstOrDefault(r => r.Id == source);
            if (targetRepoFromDb == null || sourceRepoFromDb == null)
            {
                return NotFound();
            }
            targetRepoFromDb.Content = sourceRepoFromDb.Content;

            var approvedPr = _unitOfWork.PullRequestRepository.GetFirstOrDefault(x => x.Id == prId);
            var prComments = _unitOfWork.CommentRepository.GetAll().Where(x => x.PullRequestId == prId);

            _unitOfWork.CommentRepository.RemoveRange(prComments);
            _unitOfWork.PullRequestRepository.Remove(approvedPr);
            _unitOfWork.RepoRepository.Remove(sourceRepoFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Pull Request approved successfully!";
            return RedirectToAction("Details", "Repository", new { id = target });
        }

        public IActionResult Update(string? id, string repoId)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var prFromDb = _unitOfWork.PullRequestRepository.GetFirstOrDefault(r => r.Id == id);
            var repositories = _unitOfWork.RepoRepository.GetAll();
            if (prFromDb == null)
            {
                return NotFound();
            }

            var model = new PullRequestInputViewModel
            {
                Name = prFromDb.Name,
                SourceName = repositories.FirstOrDefault(r => r.Id == repoId).Name ?? "No name...",
                Repositories = repositories,
                RepoId = repoId,
                PrId= prFromDb.Id
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(PullRequestInputViewModel model)
        {
            if (ModelState.IsValid)
            {
                var prFromDb = this._unitOfWork.PullRequestRepository.GetFirstOrDefault(r => r.Id == model.PrId);

                prFromDb.Name = model.Name;
                prFromDb.Source = model.RepoId;
                prFromDb.TargetRepository = model.TargetedRepo;

                _unitOfWork.PullRequestRepository.Update(prFromDb);

                _unitOfWork.Save();
                TempData["success"] = "Pull Request edited successfully!";
                return RedirectToAction("Index", new { id = model.RepoId });
            }
            return RedirectToAction("Update", new { id = model.PrId, repoId = model.RepoId });
        }
    }
}

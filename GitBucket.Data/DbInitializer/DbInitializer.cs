using GitBucket.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.Security;

namespace GitBucket.Data.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly GitBucketDbContext _db;

        public DbInitializer(UserManager<IdentityUser> userManager, GitBucketDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public void Initializer()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception)
            {}

            if (_userManager.Users.Any() == true)
            {
                var user = new User 
                {
                    Name = "TopG",
                    UserName = "topG@yopmail.com",
                    Email = "topG@yopmail.com"
                };
                _userManager.CreateAsync(user);

                var firstRepository = new Repository
                {
                    Id = Guid.NewGuid().ToString(),
                    Access = "Public",
                    Name = "First Random Repository",
                    Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                    UserId = user.Id,
                    User = user,
                };

                var secondRepository = new Repository
                {
                    Id = Guid.NewGuid().ToString(),
                    Access = "Public",
                    Name = "Second Random Repository",
                    Content = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.",
                    UserId = user.Id,
                    User = user
                };

                var commitToFirstRepo = new Commit
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "Random commit",
                    Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. After one TopG commit.",
                    ContentBeforeCommit = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                    Repository = firstRepository,
                    RepositoryId = firstRepository.Id,
                    User = user,
                    UserId = user.Id,
                };

                var commitToSecondRepo = new Commit
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "Random commit",
                    Content = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. After one TopG commit.",
                    ContentBeforeCommit = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.",
                    Repository = secondRepository,
                    RepositoryId = secondRepository.Id,
                    User = user,
                    UserId = user.Id,
                };

                var issueToFirstRepo = new Issue 
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "Hello this is dummy issue.",
                    Content = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.",
                    UserId= user.Id,
                    User = user,
                    Repository = firstRepository,
                    RepositoryId = firstRepository.Id,
                };

                var issueToSecondRepo = new Issue
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "Hello this is dummy issue.",
                    Content = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.",
                    UserId = user.Id,
                    User = user,
                    Repository = secondRepository,
                    RepositoryId = secondRepository.Id,
                };

                var firstPullRequest = new PullRequest 
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Dummy Title Of Pr.",
                    Source = firstRepository.Id,
                    TargetRepository = secondRepository.Id,
                    User= user,
                    UserId= user.Id,
                };

                var secondPullRequest = new PullRequest
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Dummy Title Of Pr.",
                    Source = secondRepository.Id,
                    TargetRepository = firstRepository.Id,
                    User = user,
                    UserId = user.Id,
                };

                var commentToFirstRepo = new Comment
                { 
                    Id = Guid.NewGuid().ToString(),
                    Title = "Hello from first comment...",
                    PullRequestId = firstPullRequest.Id,
                    User = user,
                    UserId = user.Id,
                };

                var commentToSecondRepo = new Comment
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "Hello from first comment...",
                    PullRequestId = secondPullRequest.Id,
                    User = user,
                    UserId = user.Id,
                };

                _db.Repository.AddRange(new[] { firstRepository, secondRepository } );
                _db.Commits.AddRange(new[] { commitToFirstRepo,commitToSecondRepo } );
                _db.Issues.AddRange(new[] { issueToFirstRepo, issueToSecondRepo });
                _db.PullRequests.AddRange(new[] { firstPullRequest, secondPullRequest });
                _db.Comments.AddRange(new[] { commentToFirstRepo, commentToSecondRepo });
                _db.SaveChanges();
            }
        }
    }
}

namespace Git.Controllers
{
    using System.Linq;

    using MyWebServer.Controllers;
    using MyWebServer.Http;

    using Git.Data;
    using Git.Data.Models;
    using Git.Models.Repositories;
    using Git.Services;

    using static Git.Data.DataConstants;

    public class RepositoriesController : Controller
    {
        private readonly GitDbContext data;
        private readonly IValidator validator;

        public RepositoriesController(
            GitDbContext data,
            IValidator validator)
        {
            this.validator = validator;
            this.data = data;
        }

        public HttpResponse All()
        {
            var repositories = this.data
                .Repositories
                .Where(r => r.IsPublic)
                .OrderByDescending(r => r.CreatedOn)
                .Select(r => new RepositoryListingViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Owner = r.Owner.Username,
                    CreatedOn = r.CreatedOn.ToString("R"),
                    Commits = r.Commits.Count(),
                })
                .ToArray();

            return View(repositories);
        }

        [Authorize]
        public HttpResponse Create() => View();

        [HttpPost]
        [Authorize]
        public HttpResponse Create(RepositoryFormModel model)
        {
            var modelErrors = this.validator.ValidateRepository(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var repository = new Repository
            {
                Name = model.Name,
                IsPublic = model.RepositoryType == RepositoryPublicType,
                OwnerId = this.User.Id
            };

            this.data.Repositories.Add(repository);

            this.data.SaveChanges();

            return Redirect("/Repositories/All");
        }

    }
}

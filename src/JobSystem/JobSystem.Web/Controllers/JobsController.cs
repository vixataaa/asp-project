using JobSystem.Data.Models;
using JobSystem.Services.Data.Contracts;
using JobSystem.ViewModels.Jobs;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobSystem.Web.Controllers
{
    public class JobsController : Controller
    {
        private readonly IJobsService jobService;
        private readonly IUsersService userService;

        // Extract in constants
        private const int DEFAULT_PAGE_SIZE = 3;

        public JobsController(IJobsService jobService, IUsersService userService)
        {
            this.jobService = jobService;
            this.userService = userService;
        }

        [HttpGet]
        public ActionResult Index(string query, int pageNumber = 1)
        {
            var jobs = this.jobService.GetAll(query, pageNumber, DEFAULT_PAGE_SIZE);

            // TODO: Mapper, extract every job to JobsListItem
            var viewModel = new JobsIndexViewModel()
            {
                Jobs = jobs,
                ItemsCount = jobs.Count(),
                CurrentPageNumber = pageNumber,
                TotalPagesCount = (int)Math.Ceiling((double)this.jobService.RecordsCount(query) / DEFAULT_PAGE_SIZE),
                Query = query
            };

            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Firm")]
        public ActionResult AddJob()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Firm")]
        [ValidateAntiForgeryToken]
        public ActionResult AddJob(AddJobViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var currentUser = this.userService.GetById(User.Identity.GetUserId());

            // TODO: Mapper
            var job = new Job
            {
                Title = model.Title,
                Description = model.Description,
                AddedBy = (Firm)currentUser
            };

            this.jobService.AddJob(job);

            return this.RedirectToAction("Index");
        }
    }
}
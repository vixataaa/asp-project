using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bytes2you.Validation;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using SecondHand.Data.Models;
using SecondHand.Services.Data.Common;
using SecondHand.Services.Data.Contracts;
using SecondHand.Web.Infrastructure;
using SecondHand.Web.Infrastructure.Attributes;
using SecondHand.Web.Models.Advertisements;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SecondHand.Web.Controllers
{
    [SaveChanges]
    public class AdvertisementsController : Controller
    {
        private const int DEFAULT_PAGE_SIZE = 6;

        private readonly IUsersService userService;
        private readonly IAdvertisementsService advertService;
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        public AdvertisementsController(IUsersService userService, IAdvertisementsService advertService, 
            ICategoryService categoryService, IMapper mapper)
        {
            Guard.WhenArgument(userService, "userService").IsNull().Throw();
            Guard.WhenArgument(advertService, "advertService").IsNull().Throw();
            Guard.WhenArgument(categoryService, "categoryService").IsNull().Throw();
            Guard.WhenArgument(mapper, "mapper").IsNull().Throw();


            this.userService = userService;
            this.advertService = advertService;
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult Index(int pageNumber = 1, int pageSize = DEFAULT_PAGE_SIZE, string query = "",
            string sortProperty = "", SortType sortType = SortType.Descending,
            string category = "")
        {
            var advertisements = this.advertService
                .GetAdvertisements(pageNumber, pageSize, query, sortProperty, sortType, category)
                .MapTo<AdvertisementListItemViewModel>()
                .ToList();

            var viewModel = new AdvertisementIndexViewModel
            {
                Advertisements = advertisements,
                PageCount = (int)Math.Ceiling((double)this.advertService.LastQueryRecordsCount / pageSize)
            };

            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public ActionResult AddAdvertisement()
        {
            var categories = this.categoryService
                .GetAll()
                .Select(x => new SelectListItem() { Text = x.Name, Value = x.Name })
                .ToList();

            var viewModel = new AdvertisementCreationViewModel
            {
                Categories = categories
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [SaveChanges]
        public ActionResult AddAdvertisement(AdvertisementCreationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var dbModel = this.mapper.Map<Advertisement>(model);
            dbModel.AddedBy = this.userService.GetByUsername(User.Identity.Name);

            this.advertService.CreateAdvertisement(dbModel, model.Category);

            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {
            var advertisement = this.advertService.GetById(id);

            if (advertisement == null)
            {
                return this.RedirectToAction("Index");
            }

            var viewModel = this.mapper.Map<AdvertisementDetailsViewModel>(advertisement);

            return this.View(viewModel);
        }

        [Authorize]
        public ActionResult MyAdvertisements()
        {
            this.ViewData["username"] = User.Identity.Name;

            return this.View();
        }

        [Authorize]
        [HttpPost]
        [SaveChanges]
        public ActionResult Delete(Guid id)
        {
            var adv = this.advertService.GetById(id);

            if (adv == null)
            {
                return this.RedirectToAction("Index");
            }

            if (adv.AddedBy.UserName != User.Identity.Name)
            {
                return this.RedirectToAction("Details", new { id = id });
            }

            this.advertService.Remove(id);

            return this.RedirectToAction("MyAdvertisements");
        }

        [Authorize]
        public ActionResult Edit(Guid id)
        {
            var adv = this.advertService.GetById(id);

            if (adv == null)
            {
                return this.RedirectToAction("Index");
            }

            if (adv.AddedBy.UserName != User.Identity.Name)
            {
                return this.RedirectToAction("Details", new { id = id });
            }

            var viewModel = this.mapper.Map<AdvertisementEditViewModel>(adv);

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [SaveChanges]
        public ActionResult Edit(AdvertisementEditViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var adv = this.advertService.GetById(model.Id);

            if (adv == null)
            {
                return this.RedirectToAction("Index");
            }

            if (User.Identity.Name != adv.AddedBy.UserName)
            {
                return this.RedirectToAction("Details", new { id = model.Id.ToString() });
            }

            adv.Title = model.Title;
            adv.Description = model.Description;
            adv.Price = model.Price;
            adv.CurrencyType = model.CurrencyType;

            this.advertService.Edit(adv);

            return this.RedirectToAction("Details", new { id = model.Id.ToString() });
        }


        public ActionResult UserAdvertisements([DataSourceRequest] DataSourceRequest request, string username)
        {
            var loggedUsername = username;

            var result = this.advertService
                .GetUserAdvertisements(loggedUsername)
                .ProjectTo<AdvertisementListItemViewModel>()
                .ToDataSourceResult(request);

            return this.Json(result);
        }
    }
}

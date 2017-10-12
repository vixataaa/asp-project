using AutoMapper;
using AutoMapper.QueryableExtensions;
using SecondHand.Data.Models;
using SecondHand.Services.Data.Contracts;
using SecondHand.Web.Infrastructure;
using SecondHand.Web.Models.Advertisements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecondHand.Web.Controllers
{
    public class AdvertisementsController : Controller
    {
        private readonly IUsersService userService;
        private readonly IAdvertisementsService advertService;
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        public AdvertisementsController(IUsersService userService, IAdvertisementsService advertService
            , ICategoryService categoryService, IMapper mapper)
        {
            this.userService = userService;
            this.advertService = advertService;
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult Index()
        {
            // Defer execution
            var advertisements = this.advertService
                .GetAdvertisements()
                .MapTo<AdvertisementListItemViewModel>()
                .ToList();



            var viewModel = new AdvertisementIndexViewModel
            {
                Advertisements = advertisements
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
            return this.View();
        }
    }
}
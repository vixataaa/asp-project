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
        private readonly IMapper mapper;

        public AdvertisementsController(IUsersService userService, IAdvertisementsService advertService, IMapper mapper)
        {
            this.userService = userService;
            this.advertService = advertService;
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
            return this.View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult AddAdvertisement(AddAdvertisementViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var dto = this.mapper.Map<Advertisement>(model);
            dto.AddedBy = this.userService.GetByUsername(User.Identity.Name);

            this.advertService.CreateAdvertisement(dto, model.Category);

            return this.RedirectToAction("Index");
        }
    }
}
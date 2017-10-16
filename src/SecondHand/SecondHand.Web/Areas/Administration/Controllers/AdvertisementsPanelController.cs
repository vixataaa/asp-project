using AutoMapper;
using Bytes2you.Validation;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using SecondHand.Data.Models;
using SecondHand.Services.Data.Contracts;
using SecondHand.Web.Areas.Administration.Controllers.Base;
using SecondHand.Web.Areas.Administration.Models.AdvertisementsPanel;
using SecondHand.Web.Infrastructure;
using SecondHand.Web.Infrastructure.Attributes;
using System.Linq;
using System.Web.Mvc;

namespace SecondHand.Web.Areas.Administration.Controllers
{
    public class AdvertisementsPanelController : AdminController
    {
        private readonly IAdvertisementsService advertisementService;
        private readonly IMapper mapper;

        public AdvertisementsPanelController(IAdvertisementsService advertisementService, IMapper mapper)
        {
            Guard.WhenArgument(advertisementService, "advertisementService").IsNull().Throw();
            Guard.WhenArgument(mapper, "mapper").IsNull().Throw();

            this.advertisementService = advertisementService;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult GetAdvertisements([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.advertisementService
                .GetAdvertisements()
                .Select(x => this.mapper.Map<AdvertisementGridViewModel>(x))
                .ToList()
                .ToDataSourceResult(request);

            return this.Json(result);
        }

        [SaveChanges]
        public ActionResult EditAdvertisement(AdvertisementGridViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dbModel = this.mapper.Map<Advertisement>(model);
                this.advertisementService.Edit(dbModel);
            }

            return this.Json(new { model });
        }

        [SaveChanges]
        public ActionResult RemoveAdvertisement(AdvertisementGridViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dbModel = this.mapper.Map<Advertisement>(model);
                this.advertisementService.Remove(dbModel);
            }

            return this.Json(new { model });
        }
    }
}
using SecondHand.Web.Areas.Administration.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecondHand.Web.Areas.Administration.Controllers
{
    public class PanelController : AdminController
    {
        public PanelController()
        {
        }

        public ActionResult Index()
        {
            return this.View();
        }
    }
}
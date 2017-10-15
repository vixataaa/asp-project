using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecondHand.Web.Areas.Administration.Controllers.Base
{
    [Authorize(Roles = "Admin")]
    public abstract class AdminController : Controller
    {
    }
}
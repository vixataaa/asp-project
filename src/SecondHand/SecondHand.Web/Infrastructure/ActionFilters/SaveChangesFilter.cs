using SecondHand.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecondHand.Web.Infrastructure.ActionFilters
{
    public class SaveChangesFilter : IActionFilter
    {
        private readonly ISaveContext saveContext;

        public SaveChangesFilter(ISaveContext saveContext)
        {
            this.saveContext = saveContext;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            this.saveContext.SaveChanges();
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }
    }
}
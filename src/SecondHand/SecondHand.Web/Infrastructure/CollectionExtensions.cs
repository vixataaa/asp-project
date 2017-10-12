using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace SecondHand.Web.Infrastructure
{
    public static class CollectionExtensions
    {
        public static RouteValueDictionary ToRouteValueDictionaryPaging(this NameValueCollection collection, int page)
        {
            var routeValueDictionary = new RouteValueDictionary();
            foreach (var key in collection.AllKeys)
            {
                if (key != "pageNumber")
                {
                    routeValueDictionary.Add(key, collection[key]);
                }
            }

            routeValueDictionary.Add("pageNumber", page);

            return routeValueDictionary;
        }
    }
}
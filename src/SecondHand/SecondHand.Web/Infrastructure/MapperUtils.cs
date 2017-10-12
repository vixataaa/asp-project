using SecondHand.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecondHand.Web.Infrastructure
{
    public static class MapperUtils
    {
        // Extract in utils
        public static List<Photo> GeneratePhotosList(params string[] photos)
        {
            var result = new List<Photo>();

            foreach (var photo in photos)
            {
                if (!string.IsNullOrEmpty(photo))
                {
                    result.Add(new Photo { Url = photo });
                }
            }

            return result;
        }
    }
}
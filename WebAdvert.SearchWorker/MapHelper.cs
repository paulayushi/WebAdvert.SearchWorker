using AdvertApi.Models.Messages;
using System;

namespace WebAdvert.SearchWorker
{
    public static class MapHelper
    {
        public static AdvertMessageModel Map(AdvertConfirmedMessage model)
        {
            var doc = new AdvertMessageModel
            {
                Id = model.Id,
                Title = model.Title,
                CreationDate = DateTime.UtcNow
            };
            return doc;
        }
    }
}

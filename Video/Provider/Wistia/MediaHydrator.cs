using System;
using System.Collections.Generic;
using System.Linq;
using Gorilla.Storage.Enums;
using Gorilla.Storage.Video.Interfaces;
using Gorilla.Storage.Video.Models;

namespace Gorilla.Storage.Video.Provider.Wistia
{
    internal static class MediaHydrator
    {
        internal static IMedia ConvertToMedia(Gorilla.Wistia.Models.Media media)
        {
            var result = new Media
            {
                Description = media.description,
                Name = media.name,
                Id = media.hashed_id,
                Progress = media.progress,
                Thumbnail = media.thumbnail?.url,
                Duration = media.duration
            };

            switch (media.status)
            {
                case "queued":
                    result.Status = UploadStatus.Queued;
                    break;
                case "processing":
                    result.Status = UploadStatus.Processing;
                    break;
                case "ready":
                    result.Status = UploadStatus.Ready;
                    break;
                case "failed":
                    result.Status = UploadStatus.Ready;
                    break;
            }

            return result;
        }

        internal static IEnumerable<IMedia> ConvertToMedia(IEnumerable<Gorilla.Wistia.Models.Media>  medias)
        {
            return medias.Select(MediaHydrator.ConvertToMedia).ToList();
        }
    }
}
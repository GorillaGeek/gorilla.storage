
using Gorilla.Storage.Enums;
using Gorilla.Storage.Video.Interfaces;

namespace Gorilla.Storage.Video.Models
{
    public class Media : IMedia
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Duration { get; set; }
        public UploadStatus Status { get; set; }
        public float Progress { get; set; }
        public string Thumbnail { get; set; }
    }
}
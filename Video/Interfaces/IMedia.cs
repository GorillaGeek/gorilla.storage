
using Gorilla.Storage.Enums;

namespace Gorilla.Storage.Video.Interfaces
{
    public interface IMedia
    {
        string Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        int Duration { get; set; }
        UploadStatus Status { get; set; }
        float Progress { get; set; }
        string Thumbnail { get; set; }
    }
}
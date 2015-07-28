
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Gorilla.Storage.Video.Interfaces
{
    public interface IProvider
    {
        Task<ICollection> Collection(string id);
        Task<IEnumerable<ICollection>> Collections(int page = 1, int pageSize = 10);
        Task<ICollection> CollectionCreate(ICollection collection);
        Task<ICollection> CollectionCreate(string name);
        Task<ICollection> CollectionUpdate(ICollection collection);
        Task<ICollection> CollectionUpdate(string id, string name);
        Task CollectionDelete(ICollection collection);
        Task CollectionDelete(string id);
        Task<IMedia> UploadFile(Stream fileStream, string name = "");
        Task<IMedia> UploadUrl(string url, string name = "");
        Task<IMedia> MediaGet(string id);
        Task<IEnumerable<IMedia>> MediaList(int page = 1, int pageSize = 10);
        Task<IMedia> MediaUpdate(string Id, string name, string description="");
        Task<IMedia> MediaUpdate(IMedia media);
        Task MediaDelete(string Id);
    }
}
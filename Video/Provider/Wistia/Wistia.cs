using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Gorilla.Storage.Video.Interfaces;
using Gorilla.Storage.Video.Models;
using Gorilla.Wistia;
using Gorilla.Wistia.Authentication;

namespace Gorilla.Storage.Video.Provider.Wistia
{
    public class Wistia : IProvider
    {
        private readonly Client _client;

        public Wistia(Client client)
        {
            _client = client;
        }

        public async Task<ICollection> Collection(string id)
        {
            var project = await _client.Project.Show(id);
            return new Collection
            {
                Id = id,
                Description = project.description,
                Name = project.name
            };
        }

        public async Task<IEnumerable<ICollection>> Collections(int page = 1, int pageSize = 10)
        {
            var projects = await _client.Project.List(page, pageSize);

            return projects.Select(project => new Collection
            {
                Name = project.name,
                Description = project.description,
                Id = project.hashedId
            }).ToList();
        }

        public async Task<ICollection> CollectionCreate(ICollection collection)
        {
            var result = await _client.Project.Create(collection.Name);
            collection.Id = result.hashedId;
            return collection;
        }

        public async Task<ICollection> CollectionCreate(string name)
        {
            var result = await _client.Project.Create(name);
            return new Collection
            {
                Id = result.hashedId,
                Name = result.name,
                Description = result.description
            };
        }

        public async Task<ICollection> CollectionUpdate(ICollection collection)
        {
            await _client.Project.Update(collection.Id, collection.Name);
            return collection;
        }

        public async Task<ICollection> CollectionUpdate(string id, string name)
        {
            var result = await _client.Project.Update(id, name);
            return new Collection
            {
                Id = result.hashedId,
                Name = result.name,
                Description = result.description
            };
        }

        public async Task CollectionDelete(ICollection collection)
        {
            await _client.Project.Delete(collection.Id);
        }

        public async Task CollectionDelete(string id)
        {
            await _client.Project.Delete(id);
        }

        public async Task<IMedia> UploadFile(Stream fileStream, string name = "")
        {
            var result = await _client.Upload.File(fileStream, name);
            return MediaHydrator.ConvertToMedia(result);
        }

        public async Task<IMedia> UploadUrl(string url, string name = "")
        {
            var result = await _client.Upload.Url(url, name);
            return MediaHydrator.ConvertToMedia(result);
        }

        public async Task<IMedia> MediaGet(string id)
        {
            var result = await _client.Media.Show(id);
            return MediaHydrator.ConvertToMedia(result);
        }

        public async Task<IEnumerable<IMedia>> MediaList(int page = 1, int pageSize = 10)
        {
            var results = await _client.Media.List(page, pageSize);
            return MediaHydrator.ConvertToMedia(results);
        }

        public async Task<IMedia> MediaUpdate(string Id, string name, string description="")
        {
            var result = await _client.Media.Update(Id, name, description);
            return new Media()
            {
                Id = result.hashed_id,
                Name = result.name,
                Description = result.description
            };
        }

        public async Task<IMedia> MediaUpdate(IMedia media)
        {
            await _client.Media.Update(media.Id, media.Name, media.Description);
            return media;
        }

        public async Task MediaDelete(string Id)
        {
            await _client.Media.Delete(Id);
        }

        public static Wistia Create(string apiPassword)
        {
            return new Wistia(new Client(new Password(apiPassword)));
        }
    }
}
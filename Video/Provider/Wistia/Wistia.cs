using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Gorilla.Storage.Video.Interfaces;
using Gorilla.Storage.Video.Models;
using Gorilla.Wistia;

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

        public async Task CollectionCreate(ICollection collection)
        {
            await _client.Project.Create(collection.Name);
        }

        public async Task CollectionCreate(string name)
        {
            await _client.Project.Create(name);
        }

        public async Task CollectionUpdate(ICollection collection)
        {
            await _client.Project.Update(collection.Id, collection.Name);
        }

        public async Task CollectionUpdate(string id, string name)
        {
            await _client.Project.Update(id, name);
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

        public async Task MediaUpdate(string Id, string name, string description="")
        {
            await _client.Media.Update(Id, name, description);
        }

        public async Task MediaUpdate(IMedia media)
        {
            await _client.Media.Update(media.Id, media.Name, media.Description);
        }

        public async Task MediaDelete(string Id)
        {
            await _client.Media.Delete(Id);
        }

    }
}
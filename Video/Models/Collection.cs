using Gorilla.Storage.Video.Interfaces;

namespace Gorilla.Storage.Video.Models
{
    public class Collection : ICollection
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
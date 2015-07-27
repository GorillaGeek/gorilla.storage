using Gorilla.Storage.Video.Interfaces;

namespace Gorilla.Storage.Video
{
    public class Video
    {

        private readonly IProvider _provider;

        public Video(IProvider provider)
        {
            _provider = provider;
        }
        
    }
}
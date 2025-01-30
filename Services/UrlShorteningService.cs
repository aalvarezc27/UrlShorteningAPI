using UrlShortening.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace UrlShortening.Services
{
    public class UrlShorteningService
    {
        public readonly IMongoCollection<Url> _urlCollection;

        public UrlShorteningService(IOptions<DatabaseSetting> urlShorteningDatabaseSetting)
        {
            var mongoClient = new MongoClient(urlShorteningDatabaseSetting.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(urlShorteningDatabaseSetting.Value.DatabaseName);
            _urlCollection = mongoDatabase.GetCollection<Url>(urlShorteningDatabaseSetting.Value.CollectionName);
        }

        public async Task<List<Url>> GetUrlsAsync() => await _urlCollection.Find(_ => true).ToListAsync();

        public async Task<Url> GetByShortCode(string shortCode) => await _urlCollection.Find(x=>x.ShortCode == shortCode).FirstOrDefaultAsync();

        public async Task Create(Url newUrl) => await _urlCollection.InsertOneAsync(newUrl);

    }
}

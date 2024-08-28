using CrossCutting;
using Domain.Entities.v1;
using Domain.Interfaces.Repositories.v1;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infraestructure.Data.Repositories.v1
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IMongoCollection<Message> _messageCollection;

        public MessageRepository(
            IMongoClient mongoClient,
            IOptions<AppSettingsConfigurations> appSettings)
        {
            var database = mongoClient.GetDatabase(appSettings.Value.MongoDBSettings.DatabaseName);
            _messageCollection = database.GetCollection<Message>(appSettings.Value.MongoDBSettings.MessagesCollection);
        }

        public async Task<IEnumerable<Message>> GetChat(string? sender = "", string? reciver = "")
        {
            var filter = Builders<Message>.Filter.Empty;

            filter = Builders<Message>.Filter.And(filter, Builders<Message>.Filter.Regex(u => u.Sender, new BsonRegularExpression(sender, "i")));
            filter = Builders<Message>.Filter.And(filter, Builders<Message>.Filter.Regex(u => u.Reciver, new BsonRegularExpression(reciver, "i")));
            
            var result = await _messageCollection.Find(filter).ToListAsync();

            var filter2 = Builders<Message>.Filter.Empty;

            filter2 = Builders<Message>.Filter.And(filter2, Builders<Message>.Filter.Regex(u => u.Sender, new BsonRegularExpression(reciver, "i")));
            filter2 = Builders<Message>.Filter.And(filter2, Builders<Message>.Filter.Regex(u => u.Reciver, new BsonRegularExpression(sender, "i")));

            var teste = await _messageCollection.Find(filter2).ToListAsync();

            result.AddRange(await _messageCollection.Find(filter2).ToListAsync());

            return result;
        }

        public async Task<bool> SendMessage(Message message)
        {
            await _messageCollection.InsertOneAsync(message);
            return true;
        }
    }
}

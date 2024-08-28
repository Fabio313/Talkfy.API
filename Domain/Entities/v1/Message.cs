using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Domain.Entities.v1
{
    public class Message
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("sender")]
        public string Sender { get; set; }

        [BsonElement("reciver")]
        public string Reciver { get; set; }

        [BsonElement("text")]
        public string Text { get; set; }

        [BsonElement("sendDate")]
        public DateTime SendDate { get; set; }
    }
}

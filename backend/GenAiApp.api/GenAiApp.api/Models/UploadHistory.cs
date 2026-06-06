using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GenAiApp.api.Models
{
    public class UploadHistory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string? Id { get; set; }
        public string FileName { get; set; }
        public string OutputType { get; set; } = string.Empty;

        public string GeneratedContent { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }
    }
}

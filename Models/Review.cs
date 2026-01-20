using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CookbookApp.Models;

public class Review
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string RecipeId { get; set; } = string.Empty;

    public string? Comment { get; set; }

    public int Rating { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

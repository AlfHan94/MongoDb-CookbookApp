using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CookbookApp.Models;

public class Recipe
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public List<string> Ingredients { get; set; } = new();

    public List<string> Steps { get; set; } = new();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

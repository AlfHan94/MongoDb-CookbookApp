using CookbookApp.Models;
using MongoDB.Driver;

namespace CookbookApp.Data;


public class MongoContext
{

    private readonly IMongoDatabase _database;

    public MongoContext()
    {
        var client = new MongoClient("mongodb://localhost:27017/");
        _database = client.GetDatabase("AlfredHandin");
    }

    public IMongoCollection<Recipe> Recipes =>
        _database.GetCollection<Recipe>("recipes");
    public IMongoCollection<Review> Reviews =>
        _database.GetCollection<Review>("reviews");

}
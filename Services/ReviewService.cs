using CookbookApp.Data;
using CookbookApp.Models;
using MongoDB.Driver;


namespace CookbookApp.Services;

public class ReviewService
{
    private readonly IMongoCollection<Review> _reviews;

    public ReviewService(MongoContext context)
    {
        _reviews = context.Reviews;
    }

    public void AddReview(Review review)
    {
        _reviews.InsertOne(review);
    }

    public List<Review> GetReviewsForRecipe(string recipeId)
    {
        var filter = Builders<Review>.Filter.Eq(r => r.RecipeId, recipeId);
        return _reviews.Find(filter).ToList();
    }

}
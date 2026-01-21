using CookbookApp.Data;
using CookbookApp.Models;
using MongoDB.Driver;

namespace CookbookApp.Services;

public class RecipeService
{
    private readonly IMongoCollection<Recipe> _recipes;

    public RecipeService(MongoContext context)
    {
        _recipes = context.Recipes;
    }

    public void AddRecipe(Recipe recipe)
    {
        _recipes.InsertOne(recipe);
    }
    public List<Recipe> GetAllRecipes()
    {
        return _recipes.Find(_ => true).ToList();
    }

    public void UpdateRecipe(string id, Recipe updateRecipe)
    {
        var filter = Builders<Recipe>.Filter.Eq(r => r.Id, id);

        _recipes.ReplaceOne(filter, updateRecipe);
    }



}

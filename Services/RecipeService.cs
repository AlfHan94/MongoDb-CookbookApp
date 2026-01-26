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

    public void UpdateRecipe(Recipe updatedRecipe)
    {
        _recipes.ReplaceOne(r => r.Id == updatedRecipe.Id, updatedRecipe);
    }


    public void DeleteRecipe(string id)
    {
        _recipes.DeleteOne(r => r.Id == id);
    }


}

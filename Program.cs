using CookbookApp.Data;
using CookbookApp.Models;
using CookbookApp.Services;


var context = new MongoContext();

var recipeService = new RecipeService(context);
var reviewService = new ReviewService(context);




while (true)
{
    Console.WriteLine();
    Console.WriteLine("Choose an option: ");
    Console.WriteLine("1. Add recipe");
    Console.WriteLine("5. Show all recipes");
    Console.WriteLine("6. Show reviews for recipe");
    Console.WriteLine("0. Exit");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "0":
            return;

        case "1":
            {
                Console.Write("Title: ");
                var title = Console.ReadLine();

                Console.WriteLine("Enter ingredients (empty line to finish):");
                var ingredients = new List<string>();
                while (true)
                {
                    var line = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(line)) break;
                    ingredients.Add(line);
                }

                Console.WriteLine("Enter steps (empty line to finish):");
                var steps = new List<string>();
                while (true)
                {
                    var line = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(line)) break;
                    steps.Add(line);
                }

                var recipe = new Recipe
                {
                    Title = title ?? "",
                    Ingredients = ingredients,
                    Steps = steps
                };

                recipeService.AddRecipe(recipe);
                Console.WriteLine("Recipe added!");
                break;
            }




        case "5":
            {
                var recipes = recipeService.GetAllRecipes();

                if (recipes.Count == 0)
                {
                    Console.WriteLine("No recipes found.");
                    break;
                }

                Console.WriteLine("Choose a recipe:");
                for (int i = 0; i < recipes.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {recipes[i].Title}");
                }
                break;
            }



    }




}


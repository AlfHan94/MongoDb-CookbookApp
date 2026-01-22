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
                Console.Clear();

                var recipes = recipeService.GetAllRecipes();

                if (recipes.Count == 0)
                {
                    Console.WriteLine("No recipes found.");
                    break;
                }

                Console.WriteLine("Recipes:");
                for (int i = 0; i < recipes.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {recipes[i].Title}");
                }

                Console.Write("Choose a recipe number to view details: ");
                var input = Console.ReadLine();

                if (!int.TryParse(input, out int choiceNumber) || choiceNumber < 1 || choiceNumber > recipes.Count)
                {
                    Console.WriteLine("Invalid choice.");
                    break;
                }

                var selected = recipes[choiceNumber - 1];

                Console.WriteLine();
                Console.WriteLine(selected.Title);
                Console.WriteLine("Ingredients:");
                foreach (var ing in selected.Ingredients)
                    Console.WriteLine($"- {ing}");

                Console.WriteLine("Steps:");
                for (int i = 0; i < selected.Steps.Count; i++)
                    Console.WriteLine($"{i + 1}. {selected.Steps[i]}");

                Console.WriteLine();
                Console.WriteLine("Press ENTER to return to main menu");
                Console.ReadLine();

                break;
            }



    }




}


using CookbookApp.Data;
using CookbookApp.Models;
using CookbookApp.Services;
using System.Linq;


var context = new MongoContext();

var recipeService = new RecipeService(context);
var reviewService = new ReviewService(context);
SeedData(recipeService, reviewService);


while (true)
{
    Console.WriteLine();
    Console.WriteLine("Choose an option: ");
    Console.WriteLine("1. Add recipe");
    Console.WriteLine("2. Show all recipes");
    Console.WriteLine("3. Remove/Update recipes");
    Console.WriteLine("4. Add review");
    Console.WriteLine("5. Show reviews");
    Console.WriteLine("0. Exit");

    var choice = Console.ReadLine();

    switch (choice)
    {
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

        case "2":
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
                Console.WriteLine("Press ENTER to return");
                Console.ReadLine();

                break;
            }

        case "3":
            {
                Console.Clear();

                var recipes = recipeService.GetAllRecipes();
                if (recipes.Count == 0)
                {
                    Console.WriteLine("No recipes found.");
                    Console.WriteLine("Press ENTER to return");
                    Console.ReadLine();
                    break;
                }

                Console.WriteLine("Choose a recipe:");
                for (int i = 0; i < recipes.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {recipes[i].Title}");
                }

                Console.Write("Enter a recipe number: ");
                var input = Console.ReadLine();

                if (!int.TryParse(input, out int recipeChoice) || recipeChoice < 1 || recipeChoice > recipes.Count)
                {
                    Console.WriteLine("Invalid choice.");
                    Console.WriteLine("Press ENTER to return");
                    Console.ReadLine();
                    break;
                }

                var selectedRecipe = recipes[recipeChoice - 1];

                Console.WriteLine();
                Console.WriteLine($"Selected: {selectedRecipe.Title}");
                Console.Clear();
                Console.WriteLine($"Recipe: {selectedRecipe.Title}");
                Console.WriteLine("1. Update recipe");
                Console.WriteLine("2. Delete recipe");
                Console.WriteLine("0. Cancel");

                var action = Console.ReadLine();

                switch (action)
                {
                    case "1":
                        {
                            Console.Clear();
                            Console.WriteLine($"Updating: {selectedRecipe.Title}");
                            Console.WriteLine();

                            Console.Write("New title (ENTER = keep): ");
                            var newTitle = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(newTitle))
                                selectedRecipe.Title = newTitle;

                            Console.WriteLine();
                            Console.WriteLine("Enter ingredients (empty line to finish). Leave empty immediately to keep current:");
                            var newIngredients = new List<string>();
                            var firstIng = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(firstIng))
                            {
                                newIngredients.Add(firstIng);
                                while (true)
                                {
                                    var line = Console.ReadLine();
                                    if (string.IsNullOrWhiteSpace(line)) break;
                                    newIngredients.Add(line);
                                }
                                selectedRecipe.Ingredients = newIngredients;
                            }

                            Console.WriteLine();
                            Console.WriteLine("Enter steps (empty line to finish). Leave empty immediately to keep current:");
                            var newSteps = new List<string>();
                            var firstStep = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(firstStep))
                            {
                                newSteps.Add(firstStep);
                                while (true)
                                {
                                    var line = Console.ReadLine();
                                    if (string.IsNullOrWhiteSpace(line)) break;
                                    newSteps.Add(line);
                                }
                                selectedRecipe.Steps = newSteps;
                            }

                            recipeService.UpdateRecipe(selectedRecipe);

                            Console.WriteLine();
                            Console.WriteLine("Recipe updated!");
                            Console.WriteLine("Press ENTER to return");
                            Console.ReadLine();
                            break;
                        }

                    case "2":
                        recipeService.DeleteRecipe(selectedRecipe.Id!);
                        Console.WriteLine("Recipe deleted.");
                        Console.WriteLine("Press ENTER to return");
                        Console.ReadLine();
                        break;

                    case "0":
                        break;

                    default:
                        Console.WriteLine("Invalid choice");
                        Console.ReadLine();
                        break;
                }

                break;
            }

        case "4":
            {
                Console.Clear();

                var recipes = recipeService.GetAllRecipes();
                if (recipes.Count == 0)
                {
                    Console.WriteLine("No recipes found.");
                    Console.WriteLine("Press ENTER to return");
                    Console.ReadLine();
                    break;
                }

                Console.WriteLine("Choose a recipe to review:");
                for (int i = 0; i < recipes.Count; i++)
                    Console.WriteLine($"{i + 1}. {recipes[i].Title}");

                Console.Write("Enter recipe number: ");
                var input = Console.ReadLine();

                if (!int.TryParse(input, out int recipeChoice) || recipeChoice < 1 || recipeChoice > recipes.Count)
                {
                    Console.WriteLine("Invalid choice.");
                    Console.WriteLine("Press ENTER to return");
                    Console.ReadLine();
                    break;
                }

                var selectedRecipe = recipes[recipeChoice - 1];

                Console.Write("Rating (1-5):");
                if (!int.TryParse(Console.ReadLine(), out int rating) || rating < 1 || rating > 5)
                {
                    Console.WriteLine("Invalid rating.");
                    Console.WriteLine("Press ENTER to return");
                    Console.ReadLine();
                    break;
                }

                Console.Write("Comment (optional):");
                var comment = Console.ReadLine();

                var review = new Review()
                {
                    RecipeId = selectedRecipe.Id!,
                    Rating = rating,
                    Comment = string.IsNullOrWhiteSpace(comment) ? null : comment
                };

                reviewService.AddReview(review);

                Console.WriteLine("Review added!");
                Console.WriteLine("Press ENTER to return");
                Console.ReadLine();
                break;
            }

        case "5":
            {
                Console.Clear();

                var recipes = recipeService.GetAllRecipes();
                if (recipes.Count == 0)
                {
                    Console.WriteLine("No recipes found.");
                    Console.WriteLine("Press ENTER to return");
                    Console.ReadLine();
                    break;
                }

                Console.WriteLine("Choose a recipe to watch their reviews:");
                for (int i = 0; i < recipes.Count; i++)
                    Console.WriteLine($"{i + 1}. {recipes[i].Title}");

                Console.Write("Enter recipe number: ");
                var input = Console.ReadLine();

                if (!int.TryParse(input, out int recipeChoice) || recipeChoice < 1 || recipeChoice > recipes.Count)
                {
                    Console.WriteLine("Invalid choice.");
                    Console.WriteLine("Press ENTER to return");
                    Console.ReadLine();
                    break;
                }

                var selectedRecipe = recipes[recipeChoice - 1];

                var reviews = reviewService.GetReviewsForRecipe(selectedRecipe.Id!);

                Console.WriteLine();
                Console.WriteLine($"Reviews for: {selectedRecipe.Title}");
                Console.WriteLine("--------------------------------");

                if (reviews.Count == 0)
                {
                    Console.WriteLine("No reviews yet.");
                    Console.WriteLine("Press ENTER to return");
                    Console.ReadLine();
                    break;
                }

                double averageRating = reviews.Average(r => r.Rating);
                Console.WriteLine($"Average rating: {averageRating:F1} / 5");
                Console.WriteLine();

                foreach (var r in reviews)
                    Console.WriteLine($"- {r.Rating}/5  {r.Comment}");

                Console.WriteLine();
                Console.WriteLine("Press ENTER to return");
                Console.ReadLine();
                break;
            }
        case "0":
            return;
    }
}

static void SeedData(RecipeService recipeService, ReviewService reviewService)
{
    var recipes = recipeService.GetAllRecipes();
    if (recipes.Count > 0)
        return;

    var pancakes = new Recipe
    {
        Title = "Pancakes",
        Ingredients = new List<string>
        {
            "2 eggs",
            "3 dl milk",
            "2 dl flour",
            "1 pinch of salt",
            "Butter for frying"
        },
        Steps = new List<string>
        {
            "Whisk eggs and milk together.",
            "Add flour and salt and whisk until smooth.",
            "Fry in butter on medium heat until golden on both sides."
        }
    };

    recipeService.AddRecipe(pancakes);

    recipes = recipeService.GetAllRecipes();
    var pancakesFromDb = recipes.First(r => r.Title == "Pancakes");

    var tomatoPasta = new Recipe
    {
        Title = "Tomato Pasta",
        Ingredients = new List<string>
        {
            "Pasta",
            "1 can crushed tomatoes",
            "1 garlic clove",
            "Olive oil",
            "Salt & pepper"
        },
        Steps = new List<string>
        {
            "Boil pasta until al dente.",
            "Fry garlic in olive oil for 30 seconds.",
            "Add tomatoes, simmer 10 minutes, season.",
            "Mix pasta with sauce."
        }
    };

    recipeService.AddRecipe(tomatoPasta);

    recipes = recipeService.GetAllRecipes();
    var pastaFromDb = recipes.First(r => r.Title == "Tomato Pasta");

    reviewService.AddReview(new Review
    {
        RecipeId = pancakesFromDb.Id!,
        Rating = 5,
        Comment = "Super easy and really tasty!"
    });

    reviewService.AddReview(new Review
    {
        RecipeId = pancakesFromDb.Id!,
        Rating = 2,
        Comment = "Too bland for my taste, needed more salt/sugar."
    });

    reviewService.AddReview(new Review
    {
        RecipeId = pastaFromDb.Id!,
        Rating = 4,
        Comment = "Nice simple weeknight meal."
    });

    reviewService.AddReview(new Review
    {
        RecipeId = pastaFromDb.Id!,
        Rating = 1,
        Comment = "Turned out watery; I should have reduced the sauce longer."
    });
}


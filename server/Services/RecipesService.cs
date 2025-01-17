using all_spice_dotnet.Models;
using all_spice_dotnet.Repositories;

namespace all_spice_dotnet.Services;

public class RecipesService
{
    public RecipesService(RecipesRepository repository)
    {
        _repository = repository;
    }
    private readonly RecipesRepository _repository;

    internal Recipe CreateRecipe(Recipe recipeData)
    {
        Recipe recipe = _repository.CreateRecipe(recipeData);
        return recipe;
    }

    internal List<Recipe> GetAllRecipes()
    {
        List<Recipe> recipes = _repository.GetAllRecipes();
        return recipes;
    }


    internal Recipe GetRecipeById(int recipeId)
    {
        Recipe recipe = _repository.GetRecipeById(recipeId);

        if (recipe == null) throw new Exception($"Invalid recipe id: {recipeId}");

        return recipe;
    }

    internal Recipe UpdateRecipe(int recipeId, string userId, Recipe recipeUpdateData)
    {
        Recipe recipe = GetRecipeById(recipeId);
        if (recipe.CreatorId != userId) throw new Exception("This is not your recipe, hands off!");
        recipe.Title = recipeUpdateData.Title ?? recipe.Title;
        recipe.Instructions = recipeUpdateData.Instructions ?? recipe.Instructions;
        recipe.Img = recipeUpdateData.Img ?? recipe.Img;
        recipe.Category = recipeUpdateData.Category ?? recipe.Category;
        _repository.UpdateRecipe(recipe);
        return recipe;
    }


    internal string DeleteRecipe(int recipeId, string userId)
    {
        Recipe recipe = GetRecipeById(recipeId);
        if (recipe.CreatorId != userId) throw new Exception("This is not your recipe");
        _repository.DeleteRecipe(recipeId);
        return $"Deleted the {recipe.Title}!";
    }
}



// CREATE TABLE recipe(
//   id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
//   created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
//   updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
//   title VARCHAR(255) NOT NULL,
//   instructions VARCHAR(5000),
//   img VARCHAR(1000) NOT NULL,
//   category ENUM('breakfast', 'lunch', 'dinner', 'snack', 'dessert'),
//   creator_id VARCHAR(255) NOT NULL,
//   Foreign Key (creator_id) REFERENCES accounts(id) ON DELETE CASCADE
// )
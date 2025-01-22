using all_spice_dotnet.Models;
using all_spice_dotnet.Repositories;

namespace all_spice_dotnet.Services;

public class IngredientsService
{
    public IngredientsService(IngredientsRepository repository, RecipesService recipesService)
    {
        _repository = repository;
        _recipesService = recipesService;
    }
    private readonly IngredientsRepository _repository;
    private readonly RecipesService _recipesService;

    internal Ingredient CreateIngredient(Ingredient ingredientData, string userId)
    {
        Recipe recipe = _recipesService.GetRecipeById(ingredientData.recipeId);
        if (recipe.CreatorId != userId) throw new Exception($"This is not your ingredient");
        Ingredient ingredient = _repository.CreateIngredient(ingredientData);
        return ingredient;
    }

    internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
    {
        List<Ingredient> ingredients = _repository.GetIngredientsByRecipeId(recipeId);
        if (ingredients == null) throw new Exception($"Invalid ingredient id: {recipeId}");
        return ingredients;
    }

    private Ingredient GetIngredientById(int ingredientId)
    {
        Ingredient ingredient = _repository.GetIngredientById(ingredientId);
        if (ingredient == null) throw new Exception($"Invalid ingredient id: {ingredientId}");
        return ingredient;
    }
    internal string DeleteIngredient(int ingredientId, string userId)
    {
        Ingredient ingredient = GetIngredientById(ingredientId);
        if (ingredient.recipeId != ingredientId) throw new Exception("This is not your ingredient");

        Recipe recipe = _recipesService.GetRecipeById(ingredient.recipeId);
        if (recipe.CreatorId != userId) throw new Exception("You can not delete another users ingredient");
        _repository.DeleteIngredient(ingredientId);
        return "Deleted the ingredient!";
    }
}
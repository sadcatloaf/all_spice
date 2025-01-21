using all_spice_dotnet.Models;
using all_spice_dotnet.Repositories;

namespace all_spice_dotnet.Services;

public class IngredientsService
{
    public IngredientsService(IngredientsRepository repository)
    {
        _repository = repository;
    }
    private readonly IngredientsRepository _repository;

    internal Ingredient CreateIngredient(Ingredient ingredientData)
    {
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
    internal string DeleteIngredient(int ingredientId, string id)
    {
        Ingredient ingredient = GetIngredientById(ingredientId);
        if (ingredient.Id != ingredientId) throw new Exception("This is not your ingredient");
        _repository.DeleteIngredient(ingredientId);
        return $"Deleted the ingredient!";
    }
}
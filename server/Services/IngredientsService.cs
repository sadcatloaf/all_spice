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
}
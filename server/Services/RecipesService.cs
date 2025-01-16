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
}
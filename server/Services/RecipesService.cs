using all_spice_dotnet.Repositories;

namespace all_spice_dotnet.Services;

public class RecipesService
{
    public RecipesService(RecipesRepository repository)
    {
        _repository = repository;
    }
    private readonly RecipesRepository _repository;
}
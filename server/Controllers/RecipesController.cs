using all_spice_dotnet.Services;

namespace all_spice_dotnet.Controllers;

[ApiController]
[Route("api/recipes")]

public class RecipesController : ControllerBase
{
    public RecipesController(RecipesService recipesService)
    {
        _recipesService = recipesService;
    }
    private readonly RecipesService _recipesService;
}
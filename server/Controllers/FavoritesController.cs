using all_spice_dotnet.Models;
using all_spice_dotnet.Services;

namespace all_spice_dotnet.Controllers;

[ApiController]
[Route("api/favorites")]

public class FavoritesController : ControllerBase
{

    public FavoritesController(FavoritesService favoritesService, Auth0Provider auth0Provider)
    {
        _favoritesService = favoritesService;
        _auth0Provider = auth0Provider;
    }

    private readonly Auth0Provider _auth0Provider;
    private readonly FavoritesService _favoritesService;

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Favorite>> CreateFavorite([FromBody] Favorite favoriteData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            favoriteData.AccountId = userInfo.Id;
            Favorite favorite = _favoritesService.CreateFavorite(favoriteData);
            return Ok(favorite);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}
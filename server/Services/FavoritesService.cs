using all_spice_dotnet.Models;
using all_spice_dotnet.Repositories;

namespace all_spice_dotnet.Services;

public class FavoritesService
{
    private readonly FavoritesRepository _repository;

    public FavoritesService(FavoritesRepository repository)
    {
        _repository = repository;
    }

    internal Favorite CreateFavorite(Favorite favoriteData)
    {
        Favorite favorite = _repository.CreateFavorite(favoriteData);
        return favorite;
    }
}
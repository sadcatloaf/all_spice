using all_spice_dotnet.Models;

namespace all_spice_dotnet.Repositories;

public class FavoritesRepository
{
    private readonly IDbConnection _db;

    public FavoritesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Favorite CreateFavorite(Favorite favoriteData)
    {
        string sql = @"
        INSERT INTO 
        favorites(recipe_id, account_id)
        VALUES(@RecipeId, @AccountId);
        SELECT * FROM favorites WHERE id = LAST_INSERT_ID();";

        Favorite favorite = _db.Query<Favorite>(sql, favoriteData).SingleOrDefault();
        return favorite;
    }
}
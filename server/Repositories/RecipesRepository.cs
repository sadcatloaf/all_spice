using all_spice_dotnet.Models;

namespace all_spice_dotnet.Repositories;

public class RecipesRepository
{
    public RecipesRepository(IDbConnection db)
    {
        _db = db;
    }
    private readonly IDbConnection _db;

    internal Recipe CreateRecipe(Recipe recipeData)
    {
        string sql = @"
        INSERT INTO

        recipes(title, instructions, category, img, creator_id)
        VALUES(@Title, @Instructions, @Category, @Img, @CreatorId);

        SELECT 
        recipes.*,
        accounts.*
        FROM recipes 
        JOIN accounts ON recipes.creator_id = accounts.id
        WHERE recipes.id = LAST_INSERT_ID();";
        Recipe recipe = _db.Query(sql, (Recipe recipe, Account account) =>
        {
            recipe.Creator = account;
            return recipe;
        }, recipeData).SingleOrDefault();
        return recipe;
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
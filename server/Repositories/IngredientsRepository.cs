using all_spice_dotnet.Models;

namespace all_spice_dotnet.Repositories;

public class IngredientsRepository
{
    public IngredientsRepository(IDbConnection db)
    {
        _db = db;
    }
    private readonly IDbConnection _db;

    internal Ingredient CreateIngredient(Ingredient ingredientData)
    {
        string sql = @"
        INSERT INTO
        ingredients(name, quantity, recipe_id)
        VALUES(@Name, @Quantity, @RecipeId);

        SELECT
        ingredients.*
        FROM ingredients
        WHERE ingredients.id = LAST_INSERT_ID();";

        Ingredient ingredient = _db.Query<Ingredient>(sql, ingredientData).SingleOrDefault();
        return ingredient;
    }
}



//   created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
//   updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
//   name VARCHAR(255) NOT NULL,
//   quantity VARCHAR(255) NOT NULL,
//   recipe_id INT NOT NULL
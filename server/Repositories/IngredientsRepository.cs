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

    internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
    {
        string sql = @"
        SELECT * FROM ingredients WHERE recipe_id = @recipeId";
        List<Ingredient> ingredients = _db.Query<Ingredient>(sql, new { recipeId }).ToList();
        return ingredients;
    }

    internal Ingredient GetIngredientById(int ingredientId)
    {
        string sql = "SELECT * FROM ingredients WHERE id = @ingredientId;";
        Ingredient ingredient = _db.Query<Ingredient>(sql, new { ingredientId }).SingleOrDefault();
        return ingredient;
    }

    internal void DeleteIngredient(int ingredientId)
    {
        string sql = "DELETE FROM ingredients WHERE id = @id LIMIT 1;";
        int rowsAffected = _db.Execute(sql, new { id = ingredientId });

        if (rowsAffected == 0) throw new Exception("Delete successful");
        if (rowsAffected > 1) throw new Exception("Calm down your delete powers to powerful");
    }

}



//   created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
//   updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
//   name VARCHAR(255) NOT NULL,
//   quantity VARCHAR(255) NOT NULL,
//   recipe_id INT NOT NULL
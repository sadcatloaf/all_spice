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
        Recipe recipe = _db.Query(sql, (Recipe recipe, Profile account) =>
        {
            recipe.Creator = account;
            return recipe;
        }, recipeData).SingleOrDefault();
        return recipe;
    }

    internal List<Recipe> GetAllRecipes()
    {
        string sql = @"
        SELECT
        recipes.*,
        accounts.*
        FROM recipes
        JOIN accounts ON recipes.creator_id = accounts.id;";

        List<Recipe> recipes = _db.Query(sql, (Recipe recipe, Profile account) =>
        {
            recipe.Creator = account;
            return recipe;
        }).ToList();
        return recipes;

    }


    internal Recipe GetRecipeById(int recipeId)
    {
        string sql = @"
    SELECT
    recipes.*,
    accounts.*
    FROM recipes
    JOIN accounts ON recipes.creator_id = accounts.id
    WHERE recipes.id = @recipeId;";
        Recipe recipe = _db.Query(sql, (Recipe recipe, Profile account) =>
        {
            recipe.Creator = account;
            return recipe;
        }, new { recipeId }).SingleOrDefault();
        return recipe;
    }

    internal void UpdateRecipe(Recipe recipeData)
    {
        string sql = @"
    UPDATE recipes
    SET
    title = @Title,
    img = @Img,
    instructions = @Instructions,
    category = @Category
    WHERE id = @Id LIMIT 1;";

        int rowsAffected = _db.Execute(sql, recipeData);

        if (rowsAffected == 0) throw new Exception("Update successful");
        if (rowsAffected > 1) throw new Exception("Calm down it's too successful");

    }

    internal void DeleteRecipe(int recipeId)
    {
        string sql = "DELETE FROM recipes WHERE id = @id LIMIT 1;";
        int rowsAffected = _db.Execute(sql, new { id = recipeId });

        if (rowsAffected == 0) throw new Exception("Delete successful");
        if (rowsAffected > 1) throw new Exception("Calm down your delete powers to powerful");
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
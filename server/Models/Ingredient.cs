using System.ComponentModel.DataAnnotations;

namespace all_spice_dotnet.Models;

public class Ingredient
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int recipeId { get; set; }
    public string name { get; set; }
    public string quantity { get; set; }

}





//   id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
//   created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
//   updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
//   name VARCHAR(255) NOT NULL,
//   quantity VARCHAR(255) NOT NULL,
//   recipe_id INT NOT NULL
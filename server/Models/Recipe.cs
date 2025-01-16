using System.ComponentModel.DataAnnotations;

namespace all_spice_dotnet.Models;

public class Recipe
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string CreatorId { get; set; }
    [MinLength(3), MaxLength(25)] public string Title { get; set; }
    [MinLength(15), MaxLength(1000)] public string Instructions { get; set; }
    [Url, MaxLength(3000)] public string Img { get; set; }
    public string Category { get; set; }
    public Profile Creator { get; set; }
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
using System.ComponentModel.DataAnnotations;

namespace all_spice_dotnet.Models;

public class Recipe
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}
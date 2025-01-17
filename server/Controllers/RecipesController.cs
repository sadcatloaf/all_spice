using System.Formats.Asn1;
using all_spice_dotnet.Models;
using all_spice_dotnet.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace all_spice_dotnet.Controllers;

[ApiController]
[Route("/api/recipes")]

public class RecipesController : ControllerBase
{
    public RecipesController(RecipesService recipesService, Auth0Provider auth0Provider)
    {
        _recipesService = recipesService;
        _auth0Provider = auth0Provider;
    }
    private readonly RecipesService _recipesService;
    private readonly Auth0Provider _auth0Provider;

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] Recipe recipeData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            recipeData.CreatorId = userInfo.Id;
            Recipe recipe = _recipesService.CreateRecipe(recipeData);
            return Ok(recipe);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet]
    public ActionResult<List<Recipe>> GetAllRecipes()
    {
        try
        {
            List<Recipe> recipes = _recipesService.GetAllRecipes();
            return Ok(recipes);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }


    [HttpGet("{recipeId}")]
    public ActionResult<Recipe> GetRecipeById(int recipeId)
    {
        try
        {
            Recipe recipe = _recipesService.GetRecipeById(recipeId);
            return Ok(recipe);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [Authorize]
    [HttpPut("{recipeId}")]
    public async Task<ActionResult<Recipe>> UpdateRecipe(int recipeId, [FromBody] Recipe recipeUpdateData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            Recipe updatedRecipe = _recipesService.UpdateRecipe(recipeId, userInfo.Id, recipeUpdateData);
            return Ok(updatedRecipe);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [Authorize]
    [HttpDelete("{recipeId}")]
    public async Task<ActionResult<string>> DeleteRecipe(int recipeId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            string message = _recipesService.DeleteRecipe(recipeId, userInfo.Id);
            return Ok(message);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
}
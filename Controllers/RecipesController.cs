using System.Net.Mime;
using ContosoRecipes.Models;
using ContosoRecipes.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ContosoRecipes.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class RecipesController : ControllerBase
{
    private readonly RecipesService _recipesService;

    public RecipesController(RecipesService recipesService) =>
        _recipesService = recipesService;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<List<Recipe>> Get([FromQuery] int count)
    {
        if (count <= 0)
        {
            throw new ArgumentException("Invalid count", nameof(count));
        }
        return await _recipesService.GetAsync(count);
    }

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Recipe>> Get(string id)
    {
        var Recipe = await _recipesService.GetAsync(id);

        if (Recipe is null)
        {
            return NotFound();
        }

        return Recipe;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Recipe newRecipe)
    {
        await _recipesService.CreateAsync(newRecipe);

        return CreatedAtAction(nameof(Get), new { id = newRecipe.RecipeId }, newRecipe);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Recipe updatedRecipe)
    {
        var Recipe = await _recipesService.GetAsync(id);

        if (Recipe is null)
        {
            return NotFound();
        }

        updatedRecipe.RecipeId = Recipe.RecipeId;

        await _recipesService.UpdateAsync(id, updatedRecipe);

        return NoContent();
    }

    [HttpPatch("{id:length(24)}")]
    public async Task<IActionResult> UpdateRecipe(string id, JsonPatchDocument<Recipe> recipeUpdates)
    {
        var Recipe = await _recipesService.GetAsync(id);

        if (Recipe is null)
        {
            return NotFound();
        }

        recipeUpdates.ApplyTo(Recipe);

        await _recipesService.UpdateAsync(id, Recipe);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var Recipe = await _recipesService.GetAsync(id);

        if (Recipe is null)
        {
            return NotFound();
        }

        await _recipesService.RemoveAsync(id);

        return NoContent();
    }
}
using ContosoRecipes.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ContosoRecipes.Services;

public class RecipesService
{
    private readonly IMongoCollection<Recipe> _recipesCollection;

    public RecipesService(
        IOptions<RecipesDatabaseSettings> recipesDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            recipesDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            recipesDatabaseSettings.Value.DatabaseName);

        _recipesCollection = mongoDatabase.GetCollection<Recipe>(
            recipesDatabaseSettings.Value.RecipesCollectionName);
    }

    public async Task<List<Recipe>> GetAsync(int count) =>
        await _recipesCollection.Find(_ => true).Limit(count).ToListAsync();

    public async Task<Recipe?> GetAsync(string id) =>
        await _recipesCollection.Find(x => x.RecipeId == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Recipe newRecipe) =>
        await _recipesCollection.InsertOneAsync(newRecipe);

    public async Task UpdateAsync(string id, Recipe updatedRecipe) =>
        await _recipesCollection.ReplaceOneAsync(x => x.RecipeId == id, updatedRecipe);

    public async Task RemoveAsync(string id) =>
        await _recipesCollection.DeleteOneAsync(x => x.RecipeId == id);
}
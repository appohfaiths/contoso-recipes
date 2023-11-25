using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace ContosoRecipes.Models
{
    public record Recipe
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("recipe_id")]
        public string? RecipeId { get; set; }

        [BsonElement("Name")]
        [JsonPropertyName("Name")]
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> Directions { get; set; }
        public IEnumerable<string> Tags { get; set; }
        [Required]
        public IEnumerable<Ingredient> Ingredients { get; set; }
        public DateTime Updated { get; set; }
    }
}
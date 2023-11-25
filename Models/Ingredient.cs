using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ContosoRecipes.Models
{
    public record Ingredient
    {
        [Required]
        public string Name { get; set; }
        public string Amount { get; set; }
        public string Unit { get; set; }
    }

}
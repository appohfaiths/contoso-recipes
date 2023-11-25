using ContosoRecipes.Services;
using ContosoRecipes.Models;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<RecipesDatabaseSettings>(builder.Configuration.GetSection("RecipesDatabase"));
builder.Services.AddSingleton<RecipesService>();

builder.Services.AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null)
    .AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Constoso Recipes API", Description = "For the love of food", Contact = new OpenApiContact { Name = "KodjoCode", Url = new Uri("https://www.linkedin.com/in/faithappoh/") }, License = new OpenApiLicense { Name = "MIT License" }, Version = "v1.0" });
}).AddSwaggerGenNewtonsoftSupport();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contoso Recipes API V1.0");
});
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

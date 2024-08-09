using Asp.Versioning.Conventions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-Api-Version"));
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var desserts = new List<Dessert>();

// Define um ApiVersionSet
var apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(1.0)
    .ReportApiVersions()
    .Build();

app.MapGet("", () => "Hello World!");

// GET api/v1/Dessert
app.MapGet("/api/v{v:apiVersion}/Dessert", () => Results.Ok(desserts))
    .WithName("GetAllDesserts")
    .WithTags("Desserts")
    .WithApiVersionSet(apiVersionSet)
    .MapToApiVersion(1.0);

// GET api/v1/Dessert/{id}
app.MapGet("/api/v{v:apiVersion}/Dessert/{id}", (Ulid id) =>
{
    var dessert = desserts.Find(d => d.Id == id);
    return dessert is not null ? Results.Ok(dessert) : Results.NotFound();
})
    .WithName("GetDessert")
    .WithTags("Desserts")
    .WithApiVersionSet(apiVersionSet)
    .MapToApiVersion(1.0);

// POST api/v1/Dessert
app.MapPost("/api/v{v:apiVersion}/Dessert", (Dessert dessert) =>
{
    desserts.Add(dessert);
    return Results.CreatedAtRoute("GetDessert", new { id = dessert.Id }, dessert);
})
    .WithName("CreateDessert")
    .WithTags("Desserts")
    .WithApiVersionSet(apiVersionSet)
    .MapToApiVersion(1.0);

// PUT api/v1/Dessert/{id}
app.MapPut("/api/v{v:apiVersion}/Dessert/{id}", (Ulid id, Dessert updatedDessert) =>
{
    var dessert = desserts.Find(d => d.Id == id);
    if (dessert is null)
    {
        return Results.NotFound();
    }
    dessert.Name = updatedDessert.Name;
    dessert.Ingredients = updatedDessert.Ingredients;
    dessert.Price = updatedDessert.Price;
    return Results.Ok(dessert);
})
    .WithName("UpdateDessert")
    .WithTags("Desserts")
    .WithApiVersionSet(apiVersionSet)
    .MapToApiVersion(1.0);

// DELETE api/v1/Dessert/{id}
app.MapDelete("/api/v{v:apiVersion}/Dessert/{id}", (Ulid id) =>
{
    var dessert = desserts.Find(d => d.Id == id);
    if (dessert is null)
    {
        return Results.NotFound();
    }
    desserts.Remove(dessert);
    return Results.NoContent();
})
    .WithName("DeleteDessert")
    .WithTags("Desserts")
    .WithApiVersionSet(apiVersionSet)
    .MapToApiVersion(1.0);

app.Run();

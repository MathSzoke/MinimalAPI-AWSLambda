namespace DessertsAPI.Controllers;

public static class DessertController
{
    public static List<Dessert> Desserts { get; set; } = [];

    public static WebApplication AddDessertController(this WebApplication app)
    {
        // Define a versão da API
        var apiVersionSet = app.NewApiVersionSet()
            .HasApiVersion(1.0)
            .ReportApiVersions()
            .Build();

        app.MapGet("", () => "Hello World!");

        // GET api/v1/Dessert
        /// <summary>
        /// Retorna todas as sobremesas disponíveis.
        /// </summary>
        /// <returns>Uma lista de sobremesas.</returns>
        /// <response code="200">Lista de sobremesas retornada com sucesso.</response>
        /// <response code="401">Não autorizado. Token JWT ausente ou inválido.</response>
        app.MapGet("/api/v{v:apiVersion}/Dessert", () => Results.Ok(Desserts))
            .WithName("GetAllDesserts")
            .WithTags("Desserts")
            .WithApiVersionSet(apiVersionSet)
            .RequireAuthorization()
            .MapToApiVersion(1.0);

        // GET api/v1/Dessert/{id}
        /// <summary>
        /// Retorna uma sobremesa específica pelo ID.
        /// </summary>
        /// <param name="id">O ID da sobremesa.</param>
        /// <returns>A sobremesa correspondente ao ID fornecido.</returns>
        /// <response code="200">Sobremesa encontrada e retornada com sucesso.</response>
        /// <response code="404">Nenhuma sobremesa foi encontrada com o ID fornecido.</response>
        /// <response code="401">Não autorizado. Token JWT ausente ou inválido.</response>
        app.MapGet("/api/v{v:apiVersion}/Dessert/{id}", (Ulid id) =>
        {
            var dessert = Desserts.Find(d => d.Id == id);
            return dessert is not null ? Results.Ok(dessert) : Results.NotFound();
        })
            .WithName("GetDessert")
            .WithTags("Desserts")
            .WithApiVersionSet(apiVersionSet)
            .RequireAuthorization()
            .MapToApiVersion(1.0);

        // POST api/v1/Dessert
        /// <summary>
        /// Cria uma nova sobremesa.
        /// </summary>
        /// <param name="dessert">Os dados da nova sobremesa.</param>
        /// <returns>A sobremesa criada, com o ID atribuído.</returns>
        /// <response code="201">Sobremesa criada com sucesso.</response>
        /// <response code="401">Não autorizado. Token JWT ausente ou inválido.</response>
        app.MapPost("/api/v{v:apiVersion}/Dessert", (Dessert dessert) =>
        {
            Desserts.Add(dessert);
            return Results.CreatedAtRoute("GetDessert", new { id = dessert.Id }, dessert);
        })
            .WithName("CreateDessert")
            .WithTags("Desserts")
            .WithApiVersionSet(apiVersionSet)
            .RequireAuthorization()
            .MapToApiVersion(1.0);

        // PUT api/v1/Dessert/{id}
        /// <summary>
        /// Atualiza uma sobremesa existente.
        /// </summary>
        /// <param name="id">O ID da sobremesa a ser atualizada.</param>
        /// <param name="updatedDessert">Os dados atualizados da sobremesa.</param>
        /// <returns>A sobremesa atualizada.</returns>
        /// <response code="200">Sobremesa atualizada com sucesso.</response>
        /// <response code="404">Nenhuma sobremesa foi encontrada com o ID fornecido.</response>
        /// <response code="401">Não autorizado. Token JWT ausente ou inválido.</response>
        app.MapPut("/api/v{v:apiVersion}/Dessert/{id}", (Ulid id, Dessert updatedDessert) =>
        {
            var dessert = Desserts.Find(d => d.Id == id);
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
            .RequireAuthorization()
            .MapToApiVersion(1.0);

        // DELETE api/v1/Dessert/{id}
        /// <summary>
        /// Remove uma sobremesa pelo ID.
        /// </summary>
        /// <param name="id">O ID da sobremesa a ser removida.</param>
        /// <returns>Resposta vazia indicando sucesso na remoção.</returns>
        /// <response code="204">Sobremesa removida com sucesso.</response>
        /// <response code="404">Nenhuma sobremesa foi encontrada com o ID fornecido.</response>
        /// <response code="401">Não autorizado. Token JWT ausente ou inválido.</response>
        app.MapDelete("/api/v{v:apiVersion}/Dessert/{id}", (Ulid id) =>
        {
            var dessert = Desserts.Find(d => d.Id == id);
            if (dessert is null)
            {
                return Results.NotFound();
            }
            Desserts.Remove(dessert);
            return Results.NoContent();
        })
            .WithName("DeleteDessert")
            .WithTags("Desserts")
            .WithApiVersionSet(apiVersionSet)
            .RequireAuthorization()
            .MapToApiVersion(1.0);

        return app;
    }
}

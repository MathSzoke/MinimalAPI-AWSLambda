namespace DessertsAPI.Extensions;

public static class AppArchitectures
{
    /// <summary>
    /// Configura os middlewares gerais da aplicação, como autenticação, autorização, redirecionamento HTTP e registra o controlador de sobremesas.
    /// </summary>
    /// <param name="app">A instância do <see cref="WebApplication"/> que está sendo configurada.</param>
    /// <returns>O <see cref="WebApplication"/> configurado.</returns>
    public static WebApplication AppGeneral(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseHttpsRedirection();

        app.AddDessertController();

        return app;
    }

    /// <summary>
    /// Configurações específicas do ambiente, como ativação do Swagger no ambiente de desenvolvimento.
    /// </summary>
    /// <param name="app">A instância do <see cref="WebApplication"/> que está sendo configurada.</param>
    /// <returns>O <see cref="WebApplication"/> configurado.</returns>
    public static WebApplication AppEnvironmentConfig(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Sobremesas v1");
                c.RoutePrefix = string.Empty;
            });
        }

        return app;
    }
}

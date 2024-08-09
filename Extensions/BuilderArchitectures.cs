namespace DessertsAPI.Extensions;

public static class BuilderArchitectures
{
    /// <summary>
    /// Configura os serviços gerais da aplicação, incluindo Swagger, autenticação e AWS.
    /// </summary>
    /// <param name="builder">O <see cref="WebApplicationBuilder"/> que está sendo configurado.</param>
    /// <returns>O <see cref="WebApplicationBuilder"/> configurado.</returns>
    public static WebApplicationBuilder BuilderGeneral(this WebApplicationBuilder builder)
    {
        builder.BuilderSwagger();
        builder.BuilderAuth();
        builder.BuilderAWS();

        builder.Services.AddEndpointsApiExplorer();

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

        return builder;
    }

    /// <summary>
    /// Configura o Swagger para a documentação da API.
    /// </summary>
    /// <param name="builder">O <see cref="WebApplicationBuilder"/> que está sendo configurado.</param>
    /// <returns>O <see cref="WebApplicationBuilder"/> configurado com o Swagger.</returns>
    public static WebApplicationBuilder BuilderSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "API de Sobremesas", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Description = "Digite qualquer valor como token",
                In = ParameterLocation.Header,
                Name = "Authorization"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return builder;
    }

    /// <summary>
    /// Configura a autenticação JWT Bearer na aplicação.
    /// </summary>
    /// <param name="builder">O <see cref="WebApplicationBuilder"/> que está sendo configurado.</param>
    /// <returns>O <see cref="WebApplicationBuilder"/> configurado com autenticação.</returns>
    public static WebApplicationBuilder BuilderAuth(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = "Bearer";
            options.DefaultChallengeScheme = "Bearer";
        })
        .AddJwtBearer("Bearer", options =>
        {
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                    if (!string.IsNullOrEmpty(token))
                    {
                        context.Principal = new ClaimsPrincipal(new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.Name, "TestUser")
                        }, "Bearer"));

                        context.Success();
                    }
                    return Task.CompletedTask;
                }
            };
        });

        builder.Services.AddAuthorization();

        return builder;
    }

    /// <summary>
    /// Configura o AWS Lambda Hosting para a aplicação.
    /// </summary>
    /// <param name="builder">O <see cref="WebApplicationBuilder"/> que está sendo configurado.</param>
    /// <returns>O <see cref="WebApplicationBuilder"/> configurado para o AWS Lambda.</returns>
    public static WebApplicationBuilder BuilderAWS(this WebApplicationBuilder builder)
    {
        builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

        return builder;
    }
}

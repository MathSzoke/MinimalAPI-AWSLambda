# Imagem base do SDK .NET para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar arquivos do projeto e restaurar dependências
COPY *.csproj ./
RUN dotnet restore

# Copiar todo o código-fonte e build da aplicação
COPY . ./
RUN dotnet publish -c Release -o out

# Imagem base do ASP.NET Core para runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Comando de entrada para iniciar a aplicação
ENTRYPOINT ["dotnet", "DessertsAPI.dll"]

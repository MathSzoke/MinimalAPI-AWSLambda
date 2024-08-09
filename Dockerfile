# Imagem base do SDK .NET para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar arquivos do projeto e restaurar depend�ncias
COPY *.csproj ./
RUN dotnet restore

# Copiar todo o c�digo-fonte e build da aplica��o
COPY . ./
RUN dotnet publish -c Release -o out

# Imagem base do ASP.NET Core para runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Comando de entrada para iniciar a aplica��o
ENTRYPOINT ["dotnet", "DessertsAPI.dll"]

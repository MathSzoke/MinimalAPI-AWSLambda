# Minimal API - Dessert Management

## Descrição

Este projeto é uma API Minimal desenvolvida para gerenciar sobremesas. A API utiliza o ASP.NET Core e é configurada para suportar versionamento de API. O projeto também está configurado para ser implantado na AWS Lambda usando o comando `dotnet lambda deploy-function`.

## Funcionalidades

- **GET** `/api/v{v:apiVersion}/Dessert` - Obtém todas as sobremesas.
- **GET** `/api/v{v:apiVersion}/Dessert/{id}` - Obtém uma sobremesa específica pelo ID.
- **POST** `/api/v{v:apiVersion}/Dessert` - Adiciona uma nova sobremesa.
- **PUT** `/api/v{v:apiVersion}/Dessert/{id}` - Atualiza uma sobremesa existente.
- **DELETE** `/api/v{v:apiVersion}/Dessert/{id}` - Remove uma sobremesa pelo ID.

## Configuração

### Requisitos

- .NET 8 SDK
- AWS CLI
- AWS Lambda Tools for .NET CLI (`Amazon.Lambda.Tools`)

### Instalação

1. **Clone o repositório:**

    ```bash
    git clone <URL_DO_REPOSITORIO>
    cd <NOME_DO_REPOSITORIO>
    ```

2. **Restaure as dependências:**

    ```bash
    dotnet restore
    ```

3. **Execute a aplicação localmente:**

    ```bash
    dotnet run
    ```

   A aplicação estará disponível em `https://localhost:5001`.

## Implantação na AWS Lambda

### Preparação

Certifique-se de que você tem o [AWS CLI](https://aws.amazon.com/cli/) e o [AWS Lambda Tools for .NET CLI](https://docs.aws.amazon.com/lambda/latest/dg/csharp-package.html) instalados e configurados.

### Configuração do Projeto

1. **Adicione a ferramenta AWS Lambda ao seu projeto:**

    ```bash
    dotnet tool install -g Amazon.Lambda.Tools
    ```

2. **Configure o arquivo `aws-lambda-tools-defaults.json`** com as informações da sua função Lambda.

### Comandos úteis

1. **Empacote a função Lambda:**

    ```bash
    dotnet lambda package
    ```

   Este comando cria um pacote do seu projeto que pode ser implantado na AWS Lambda.

2. **Implante a função Lambda:**

    ```bash
    dotnet lambda deploy-function <NOME_DA_FUNCAO> --package <CAMINHO_PARA_O_PACOTE>
    ```

   Substitua `<NOME_DA_FUNCAO>` pelo nome da sua função Lambda e `<CAMINHO_PARA_O_PACOTE>` pelo caminho para o pacote criado pelo comando `dotnet lambda package`.

### Comandos Úteis

- **Para ver todas as rotas disponíveis:**

    ```bash
    dotnet run
    ```

- **Para visualizar a documentação Swagger (em desenvolvimento):**

    Acesse `https://localhost:5001/swagger` no seu navegador.

## Documentação

A API está documentada usando Swagger. A documentação pode ser visualizada na URL `https://localhost:5001/swagger` quando a aplicação estiver em execução no ambiente de desenvolvimento.

## Contribuições

Se você deseja contribuir para este projeto, sinta-se à vontade para abrir uma issue ou enviar um pull request.
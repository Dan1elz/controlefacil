# API de Controle Fácil - Projeto Acadêmico

Este projeto é uma API desenvolvida em **.NET 8.0**utilizando **Entity Framework Core** para persistência de dados. Ela foi criada como parte de um projeto acadêmico para fornecer funcionalidades robustas de gerenciamento e controle de dados.

-----

## Índice

  - [Pré-requisitos](https://www.google.com/search?q=%23pr%C3%A9-requisitos)
  - [Clonando o Projeto](https://www.google.com/search?q=%23clonando-o-projeto)
  - [Configuração do Ambiente](https://www.google.com/search?q=%23configura%C3%A7%C3%A3o-do-ambiente)
      - [String de Conexão do Banco de Dados](https://www.google.com/search?q=%23string-de-conex%C3%A3o-do-banco-de-dados)
      - [Chave Secreta JWT](https://www.google.com/search?q=%23chave-secreta-jwt)
  - [Executando as Migrações](https://www.google.com/search?q=%23executando-as-migra%C3%A7%C3%B5es)
  - [Rodando a API](https://www.google.com/search?q=%23rodando-a-api)
  - [Documentação da API (Swagger/OpenAPI)](https://www.google.com/search?q=%23documenta%C3%A7%C3%A3o-da-api-swaggeropenapi)
  - [Dicas Úteis](https://www.google.com/search?q=%23dicas-%C3%BAteis)
  - [Problemas Comuns](https://www.google.com/search?q=%23problemas-comuns)
  - [Licença](https://www.google.com/search?q=%23licen%C3%A7a)

-----

## Pré-requisitos

Para baixar e executar esta API em sua máquina, certifique-se de ter os seguintes softwares instalados:

  - **SDK do .NET 6.0 ou superior:** Esta API é construída com o .NET 6.0. Você pode baixar a versão mais recente em [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download).
 - **PostgreSQL:** A API utiliza PostgreSQL como banco de dados principal. Você precisará ter uma instância do PostgreSQL rodando e as credenciais de acesso. Considerar o uso de um container Docker para o PostgreSQL é uma excelente opção para facilitar a configuração e isolamento.
 - **Docker (Opcional, mas recomendado para o banco de dados):** Para rodar o banco de dados em um container. Baixe em [https://www.docker.com/get-started](https://www.docker.com/get-started).
  - **Git:** Para clonar o repositório do projeto. Você pode baixá-lo em [https://git-scm.com/downloads](https://git-scm.com/downloads).

-----

## Clonando o Projeto

Para obter uma cópia local do projeto, utilize o seguinte comando no seu terminal:

```bash
git clone https://github.com/Dan1elz/dcontrolefacilolt
cd controlefacil
```

-----

## Configuração do Ambiente

Após clonar o repositório, você precisará configurar o arquivo `appsettings.json` na raiz do projeto. Este arquivo contém as configurações essenciais para o funcionamento da API, como a string de conexão com o banco de dados e a chave secreta para JWT.

Aqui está a estrutura do `appsettings.json` que você precisa preencher:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
      "DefaultConnection": "sua string do banco"
  },
  "JwtSettings": {
    "Key": "sua senha do jwt"
  }
}
```

### String de Conexão do Banco de Dados

Dentro da seção `"ConnectionStrings"`, localize `"DefaultConnection"`. Você deve substituir `"sua string do banco"` pela string de conexão real do seu banco de dados.

    ```
  * **PostgreSQL:**
    ```
    "Host=localhost;Port=5432;Database=NomeDoSeuBanco;Username=seu_usuario;Password=sua_senha;"
    ```
### Chave Secreta JWT

Na seção `"JwtSettings"`, localize `"Key"`. Substitua `"sua senha do jwt"` por uma **chave secreta forte e única**. Esta chave é fundamental para a segurança da sua aplicação, pois é utilizada para assinar e validar os JSON Web Tokens (JWT) que a API emitirá para autenticação de usuários. Uma boa prática é usar uma string longa e alfanumérica.

-----

## Executando as Migrações

Após configurar o `appsettings.json`, você precisará aplicar as migrações do Entity Framework Core para criar ou atualizar o esquema do banco de dados.

Abra o terminal na pasta raiz do projeto e execute os seguintes comandos:

```bash
dotnet restore
dotnet ef database update
```

  - `dotnet restore`: Este comando garante que todas as dependências do projeto estão instaladas.
  - `dotnet ef database update`: Este comando aplica as migrações pendentes, criando as tabelas necessárias no seu banco de dados conforme definido no seu código.

-----

## Rodando a API

Para iniciar o servidor de desenvolvimento da API, use o seguinte comando no terminal na raiz do projeto:

```bash
dotnet watch --urls=http://localhost:3000
```

Este comando iniciará a API e ela estará acessível em **http://localhost:3000**. O `dotnet watch` também é muito útil, pois monitora automaticamente as alterações nos seus arquivos e reinicia a API, agilizando o processo de desenvolvimento.

-----

## Documentação da API (Swagger/OpenAPI)

A API utiliza o Swagger/OpenAPI para gerar uma documentação interativa dos seus endpoints. Após iniciar o servidor, você poderá acessar a documentação em:

[http://localhost:3000/swagger](https://www.google.com/search?q=http://localhost:3000/swagger)

Nessa página, você poderá visualizar todos os endpoints disponíveis, seus métodos HTTP, parâmetros de entrada, e testá-los diretamente no navegador.

-----

## Licença

Este projeto está licenciado sob a [MIT License](https://www.google.com/search?q=LICENSE).

-----

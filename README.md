# 🎬 MovieBox — Atividade 1 (2 containers)

Aplicação web já pronta para o laboratório de Docker Compose.

## Stack da aplicação

- .NET 8 / ASP.NET Core MVC
- C#
- MySQL
- MySqlConnector 2.6.2

> O objetivo da atividade **não é desenvolver a aplicação**. O código está pronto. O desafio é conteinerizar a solução.

## Arquitetura esperada

```text
Usuário
  ↓
MovieBox (.NET)
  ↓
MySQL
```

Ao final deverão existir **2 containers**:

1. `app` — aplicação .NET
2. `database` — MySQL

## O que NÃO foi entregue

- Dockerfile
- docker-compose.yml

Esses arquivos deverão ser criados pelo grupo.

## Variáveis esperadas pela aplicação

A aplicação lê estas variáveis de ambiente:

```text
DB_HOST
DB_PORT
DB_NAME
DB_USER
DB_PASSWORD
```

Valores padrão usados quando as variáveis não existem:

```text
DB_HOST=localhost
DB_PORT=3306
DB_NAME=moviebox
DB_USER=root
DB_PASSWORD=root
```

Dentro do Docker Compose, atenção especial ao `DB_HOST`: o backend precisa encontrar o container do banco pela rede Docker.

## Porta da aplicação

Ao executar localmente, o ASP.NET Core pode escolher as portas definidas pelo ambiente de desenvolvimento. No container, o grupo deverá definir explicitamente a URL/porta utilizada pela aplicação, por exemplo com `ASPNETCORE_URLS`.

## Funcionalidades

- cadastrar filme;
- listar filmes;
- atribuir nota de 0 a 10;
- excluir filme.

A tabela `filmes` é criada automaticamente quando a aplicação consegue se conectar ao MySQL.

## Estrutura

```text
moviebox/
├── Controllers/
├── Models/
├── Repositories/
├── Views/
├── wwwroot/
├── Program.cs
├── MovieBox.csproj
└── appsettings.json
```

## Execução local (fora do Docker)

Requer .NET SDK 8+ e uma instância MySQL acessível.

```bash
dotnet restore
dotnet run
```

## Missão do grupo

Criar:

```text
Dockerfile
docker-compose.yml
```

O Compose deverá iniciar a aplicação .NET e o MySQL, configurar variáveis de ambiente e garantir persistência do banco utilizando volume.

### Conceitos esperados

```yaml
services:
build:
image:
ports:
environment:
depends_on:
volumes:
```

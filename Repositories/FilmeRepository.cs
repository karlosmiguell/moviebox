using MovieBox.Models;
using MySqlConnector;

namespace MovieBox.Repositories;

public class FilmeRepository
{
    private readonly IConfiguration _configuration;

    public FilmeRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private string ConnectionString
    {
        get
        {
            var host = _configuration["DB_HOST"] ?? "localhost";
            var port = _configuration["DB_PORT"] ?? "3306";
            var database = _configuration["DB_NAME"] ?? "moviebox";
            var user = _configuration["DB_USER"] ?? "root";
            var password = _configuration["DB_PASSWORD"] ?? "root";

            return $"Server={host};Port={port};Database={database};User ID={user};Password={password};Allow User Variables=true;";
        }
    }

    private async Task<MySqlConnection> OpenConnectionAsync()
    {
        var connection = new MySqlConnection(ConnectionString);
        await connection.OpenAsync();
        await EnsureTableAsync(connection);
        return connection;
    }

    private static async Task EnsureTableAsync(MySqlConnection connection)
    {
        const string sql = """
            CREATE TABLE IF NOT EXISTS filmes (
                id INT NOT NULL AUTO_INCREMENT,
                nome VARCHAR(120) NOT NULL,
                categoria VARCHAR(80) NOT NULL,
                nota DECIMAL(4,2) NOT NULL,
                PRIMARY KEY (id)
            );
            """;

        await using var command = new MySqlCommand(sql, connection);
        await command.ExecuteNonQueryAsync();
    }

    public async Task<List<Filme>> ListarAsync()
    {
        await using var connection = await OpenConnectionAsync();
        const string sql = "SELECT id, nome, categoria, nota FROM filmes ORDER BY id DESC";
        await using var command = new MySqlCommand(sql, connection);
        await using var reader = await command.ExecuteReaderAsync();

        var filmes = new List<Filme>();
        while (await reader.ReadAsync())
        {
            filmes.Add(new Filme
            {
                Id = reader.GetInt32("id"),
                Nome = reader.GetString("nome"),
                Categoria = reader.GetString("categoria"),
                Nota = reader.GetDecimal("nota")
            });
        }

        return filmes;
    }

    public async Task AdicionarAsync(Filme filme)
    {
        await using var connection = await OpenConnectionAsync();
        const string sql = "INSERT INTO filmes (nome, categoria, nota) VALUES (@nome, @categoria, @nota)";
        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@nome", filme.Nome);
        command.Parameters.AddWithValue("@categoria", filme.Categoria);
        command.Parameters.AddWithValue("@nota", filme.Nota);
        await command.ExecuteNonQueryAsync();
    }

    public async Task ExcluirAsync(int id)
    {
        await using var connection = await OpenConnectionAsync();
        const string sql = "DELETE FROM filmes WHERE id = @id";
        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        await command.ExecuteNonQueryAsync();
    }
}

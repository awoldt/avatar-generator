using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Npgsql;


public class Database
{
  private readonly string ConnectionString;

  public Database(IConfiguration config)
  {
    ConnectionString = config.GetConnectionString("db_connection_string");
  }

  public async Task<NpgsqlConnection> ConnectToDatabase()
  {
    var dataSourceBuilder = new NpgsqlDataSourceBuilder(ConnectionString);
    var dataSource = dataSourceBuilder.Build();

    return await dataSource.OpenConnectionAsync();
  }

  public async Task SaveAvatar(AvatarDetails data)
  {
    /* 
      Saves avatar generated to database
    */

    Console.WriteLine("\nsaving avatar to database!");

    try
    {
      using var conn = await ConnectToDatabase();
      var query = "INSERT INTO \"Avatars\" (query, s3_url, created_on) VALUES (@query, @s3_url, @created_on);";
      using var command = new NpgsqlCommand(query, conn);
      command.Parameters.AddWithValue("@query", data.Query);
      command.Parameters.AddWithValue("@s3_url", data.S3Url);
      command.Parameters.AddWithValue("@created_on", data.CreatedOn);
      await command.ExecuteNonQueryAsync();
    }
    catch (Exception err)
    {
      Console.WriteLine(err.ToString());
    }
  }

}

public class AvatarDetails
{
  [Column("id")]
  public int Id { get; set; }

  [Column("query")]
  public string Query { get; set; }

  [Column("s3_url")]
  public string S3Url { get; set; }

  [Column("created_on")]
  public DateTime CreatedOn { get; set; }
}
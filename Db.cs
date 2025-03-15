using Npgsql;

public class Db
{
    private NpgsqlDataSource _db { get; set; }

    public Db(NpgsqlDataSource db)
    {
        _db = db;
    }

    public async Task SaveToDb(string imagePrompt, string imageUrl)
    {
        try
        {
            // establish a new connection
            var conn = await _db.OpenConnectionAsync();
            var cmd = new NpgsqlCommand($@"
                INSERT INTO generated_images(prompt, image_url)
                VALUES(@prompt, @img_url);
            ", conn);
            cmd.Parameters.AddWithValue("prompt", imagePrompt);
            cmd.Parameters.AddWithValue("img_url", imageUrl);

            await cmd.ExecuteNonQueryAsync();

            await conn.CloseAsync();
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}
using System.Text.Json;
using Npgsql;

public class Db
{
    private NpgsqlDataSource _db { get; set; }

    public Db(NpgsqlDataSource db)
    {
        _db = db;
    }

    public async Task SaveToDb(Avatar avatar)
    {
        try
        {
            await using var conn = await _db.OpenConnectionAsync();
            var cmd = new NpgsqlCommand($@"
                INSERT INTO images(image_url, image_details)
                VALUES(@img_url, @image_details);
            ", conn);
            cmd.Parameters.AddWithValue("img_url", avatar.ImageUrl);
            cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "image_details", NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Jsonb, Value = JsonSerializer.Serialize(avatar.ImgageDetails) });

            await cmd.ExecuteNonQueryAsync();
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task<Avatar[]?> GetGalleryImages()
    {
        List<Avatar> avatars = [];
        // gets the latest 25 avatars generated
        try
        {
            await using var conn = await _db.OpenConnectionAsync();
            var cmd = new NpgsqlCommand($@"
               SELECT * FROM images 
               ORDER BY created_at DESC
               limit 100;
            ", conn);

            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                avatars.Add(new Avatar { ImageUrl = reader.GetString(reader.GetOrdinal("image_url")), ImgageDetails = JsonSerializer.Deserialize<AvatarDetails>(reader.GetString(reader.GetOrdinal("image_details"))) });
            }

            return avatars.ToArray();
        }
        catch (System.Exception)
        {
            return null;    
        }
    }



}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

public class db : DbContext
{
    private readonly IConfiguration _config;

    public DbSet<Avatar> Avatars { get; set; }

    public db(IConfiguration config)
    {
        _config = config;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_config["db_connection_string"]);
    }
}

public class Avatar
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("query")]
    public string Query { get; set; }

    [Column("s3_url")]
    public string S3Url { get; set; }

    [Column("created_on")]
    public DateTime CreatedOn { get; set; }
}
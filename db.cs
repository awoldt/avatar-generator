using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

public class Db : DbContext
{
    public DbSet<Avatar> Avatars { get; set; }

    public Db(DbContextOptions<Db> options)
      : base(options)
    {

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
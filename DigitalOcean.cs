using Amazon.S3;

public class DigitalOcean
{
    public AmazonS3Client _s3Client { get; set; }
    private char[] _chars =
    [
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
        'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
        'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
        '-', '_', '.', '~'
    ];

    public DigitalOcean(IConfiguration config)
    {
        AmazonS3Config s3Config = new AmazonS3Config();
        s3Config.ServiceURL = "https://nyc3.digitaloceanspaces.com";
        string accessKey = config["digital_ocean_spaces_access_key"]!;
        string secretKey = config["digital_ocean_spaces_secret_key"]!;

        _s3Client = new AmazonS3Client(accessKey, secretKey, s3Config);
    }

    public async Task<string> UploadImage(Stream imageStream)
    {
        string filename = GenerateRandomFilename();
        
        try
        {
            await _s3Client.PutObjectAsync(new Amazon.S3.Model.PutObjectRequest
            {
                BucketName = "static-assets-gu8wqg",
                Key = $"avatar/{filename}",
                InputStream = imageStream,
                CannedACL = S3CannedACL.PublicRead
            });

            return $"https://static-assets-gu8wqg.nyc3.cdn.digitaloceanspaces.com/avatar/{filename}";
        }
        catch (System.Exception)
        {
            throw;
        }

    }

    private string GenerateRandomFilename()
    {
        Random r = new Random();
        string s = "";
        for (int i = 0; i < 6; i++)
        {
            s += _chars[r.Next(_chars.Length)];
        }

        s += $"_{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}"; // now add epoch to ensure uniqueness of this filename
        s += ".png"; // add the png file extension

        return s;
    }
}
using Uploadcare;

namespace Music.Services
{
    public class UploadcareService
    {
        private readonly UploadcareClient _client;

        public UploadcareService(IConfiguration config)
        {
            _client = new UploadcareClient(
                config["Uploadcare:PublicKey"],
                config["Uploadcare:PrivateKey"]);
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Файл не выбран");

            if (file.Length > 10 * 1024 * 1024) // 10MB
                throw new ArgumentException("Размер файла превышает 10MB");

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
                throw new ArgumentException("Допустимы только JPG/PNG");

            using var stream = file.OpenReadStream();
            var result = await _client.Files.UploadAsync(stream, file.FileName);
            return result.Url;
        }
    }
}

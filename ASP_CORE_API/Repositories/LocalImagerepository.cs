using ASP_CORE_API.Data;
using ASP_CORE_API.Models.Domain;

namespace ASP_CORE_API.Repositories
{
    public class LocalImagerepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IRWalksDbContext dbContext;

        public LocalImagerepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, IRWalksDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }
        public async Task<Image> Upload(Image image)
        {
            // Define the local file path
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");

            // Ensure the "Images" directory exists
            var directoryPath = Path.GetDirectoryName(localFilePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Upload image to the local path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            // Generate URL for the uploaded file
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

            image.FilePath = urlFilePath;

            // Save image details to the database
            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();

            return image;
        }

    }
}

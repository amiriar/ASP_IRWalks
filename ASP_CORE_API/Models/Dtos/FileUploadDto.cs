using System.ComponentModel.DataAnnotations;

namespace ASP_CORE_API.Models.Dtos
{
    public class FileUploadDto
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
    }
}

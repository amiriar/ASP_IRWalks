using ASP_CORE_API.Models.Domain;
using ASP_CORE_API.Models.Dtos;
using ASP_CORE_API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP_CORE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] FileUploadDto fileUploadDto)
        {
            ValidateFileUpload(fileUploadDto);

            if (ModelState.IsValid)
            {
                var imageDoaminModel = new Image
                {
                    File = fileUploadDto.File,
                    FileDescription = fileUploadDto.FileDescription,
                    FileExtension = Path.GetExtension(fileUploadDto.File.FileName),
                    FileSizeInBytes = fileUploadDto.File.Length,
                    FileName = fileUploadDto.FileName,
                };

                 await imageRepository.Upload(imageDoaminModel);

                return Ok(imageDoaminModel);

            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(FileUploadDto fileUploadDto)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (allowedExtensions.Contains(Path.GetExtension(fileUploadDto.File.FileName)) == false)
            {
                ModelState.AddModelError("file", "File Extension Not Supported");
            }

            if (fileUploadDto.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File Size More Than 10MB Use Smaller File");
            }
        }
    }
}

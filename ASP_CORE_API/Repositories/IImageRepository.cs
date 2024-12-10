using ASP_CORE_API.Models.Domain;

namespace ASP_CORE_API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}

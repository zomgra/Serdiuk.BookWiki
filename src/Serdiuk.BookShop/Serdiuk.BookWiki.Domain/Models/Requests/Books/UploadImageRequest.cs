using Microsoft.AspNetCore.Http;

namespace Serdiuk.BookShop.Domain.Models.Requests.Books
{
    public class UploadImageRequest
    {
        public Guid Id { get; set; }
        public IFormFile File { get; set; }
    }
}

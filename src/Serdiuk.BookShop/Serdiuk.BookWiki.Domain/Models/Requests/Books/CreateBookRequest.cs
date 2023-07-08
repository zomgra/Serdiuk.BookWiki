using Microsoft.AspNetCore.Http;

namespace Serdiuk.BookShop.Domain.Models.Requests.Books
{
    public class CreateBookRequest
    {
        public Guid[] Authors { get; set; }
        public IFormFile File { get; set; }
        public string Name { get; set; }
        public BookStatus Status { get; set; }
        public string Description { get; set; }
        
    }
}

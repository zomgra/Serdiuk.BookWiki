namespace Serdiuk.BookShop.Domain.Models.Requests.Authors
{
    public class UpdateAuthorRequest
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

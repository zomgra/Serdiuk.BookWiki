using Serdiuk.BookShop.Domain.IdentityModels;

namespace Serdiuk.Services.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserById(string id);
    }
}

using Application.Models.ViewModels;

namespace Application.Services.Repositories
{
    public interface IAuthLoginService
    {
        Task<bool> LoginResult(LoginViewModel login);
    }
}

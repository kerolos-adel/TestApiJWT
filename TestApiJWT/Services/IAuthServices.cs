using TestApiJWT.ViewModel;

namespace TestApiJWT.Services
{
    public interface IAuthServices
    {
        Task<AuthModel> Register(RegisterModel model);
        Task<AuthModel> Login(TokenRequestModel model);
        Task<string>AddRole(AddRoleModel model);

    }
}

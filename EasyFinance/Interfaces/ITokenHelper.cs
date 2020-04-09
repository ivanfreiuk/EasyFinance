using EasyFinance.DataAccess.Identity;

namespace EasyFinance.Interfaces
{
    public interface ITokenHelper
    {
        string GetToken(User user);
    }
}

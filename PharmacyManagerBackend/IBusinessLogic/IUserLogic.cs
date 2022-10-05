using Domain;

namespace IBusinessLogic;

public interface IUserLogic
{
    User Create(User user);
    User GetFirst(Func<User, bool> expresion);
}
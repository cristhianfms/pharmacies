using Domain;

namespace IBusinessLogic;

public interface IUserLogic
{
    User Create(User user);
    User GetUserByUserName(string userName);
}
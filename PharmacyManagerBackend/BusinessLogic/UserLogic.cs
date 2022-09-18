using Domain;
using IBusinessLogic;
using IDataAccess;

namespace BusinessLogic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserRepository _userRepository;

        public UserLogic() { }

        public UserLogic(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public User Create(User user)
        {
            User createdUser = _userRepository.Create(user);

            return createdUser;
        }

        public virtual User GetUserByUserName(string userName)
        {
            return null;
        }
    }
}

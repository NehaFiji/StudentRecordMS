using StudentRecordMS.Models;
using StudentRecordMS.Repositories;

namespace StudentRecordMS.Services
{
    public class UserServicesImpl : IUserServices
    {
        // Field
        private readonly IUserRepository _userRepository;

        // DI
        public UserServicesImpl(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public TblUser AuthenticateUserNameAndPassword(string userName, string password)
        {
            return _userRepository.AuthenticateUser(userName, password);
        }
    }
}

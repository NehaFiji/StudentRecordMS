using StudentRecordMS.Models;

namespace StudentRecordMS.Services
{
    public interface IUserServices
    {
        TblUser AuthenticateUserNameAndPassword(string userName, string password);
    }
}

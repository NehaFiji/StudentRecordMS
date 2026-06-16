using StudentRecordMS.Models;

namespace StudentRecordMS.Repositories
{
    public interface IUserRepository
    {
        TblUser AuthenticateUser(string username, string password);
    }
}

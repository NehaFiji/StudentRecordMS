using StudentRecordMS.Models;

namespace StudentRecordMS.Repositories
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetAllStudents();
        Student GetStudentByRollNumber(string rollNumber);
        Student GetStudentByUserId(int userId);
        void AddStudent(Student student);
        void UpdateStudentMarks(Student student);
        void DeleteStudent(string rollNumber);
        string GetNextRollNumber();
    }
}

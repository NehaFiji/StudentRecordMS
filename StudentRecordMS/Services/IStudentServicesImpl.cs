using StudentRecordMS.Models;
using StudentRecordMS.Repositories;

namespace StudentRecordMS.Services
{
    public class StudentServicesImpl : IStudentServices
    {
        // Field
        private readonly IStudentRepository _studentRepository;
        //dependency injection
        public StudentServicesImpl(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        #region Get All Students
        public IEnumerable<Student> GetAllStudents()
        {
            return _studentRepository.GetAllStudents();
        }
        #endregion

        #region Get Student By RollNumber
        public Student GetStudentByRollNumber(string rollNumber)
        {
            return _studentRepository.GetStudentByRollNumber(rollNumber);
        }
        #endregion

        #region Get Student By UserId
        public Student GetStudentByUserId(int userId)
        {
            return _studentRepository.GetStudentByUserId(userId);
        }
        #endregion

        #region Add Student
        public void AddStudent(Student student)
        {
            _studentRepository.AddStudent(student);
        }
        #endregion

        #region Update Student Marks
        public void UpdateStudentMarks(Student student)
        {
            _studentRepository.UpdateStudentMarks(student);
        }
        #endregion

        #region Delete Student
        public void DeleteStudent(string rollNumber)
        {
            _studentRepository.DeleteStudent(rollNumber);
        }
        #endregion

        #region Get Next Roll Number
        public string GetNextRollNumber()
        {
            return _studentRepository.GetNextRollNumber();
        }
        #endregion
    }
}

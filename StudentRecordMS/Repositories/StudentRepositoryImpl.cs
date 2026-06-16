using Microsoft.Data.SqlClient;
using StudentRecordMS.Models;
using System.Data;

namespace StudentRecordMS.Repositories
{
    public class StudentRepositoryImpl : IStudentRepository
    {
        // Field
        private readonly string connectionString;

        // DI
        public StudentRepositoryImpl(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("ConnStrMVC");
        }

        #region Get All Students
        public IEnumerable<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                if (connection != null)
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("sp_GetAllStudents", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Student student = new Student
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                RollNumber = reader["RollNumber"].ToString(),
                                Name = reader["Name"].ToString(),
                                Maths = Convert.ToInt32(reader["Maths"]),
                                Physics = Convert.ToInt32(reader["Physics"]),
                                Chemistry = Convert.ToInt32(reader["Chemistry"]),
                                English = Convert.ToInt32(reader["English"]),
                                Programming = Convert.ToInt32(reader["Programming"]),
                                IsActive = Convert.ToBoolean(reader["IsActive"])
                            };
                            students.Add(student);
                        }
                    }
                    connection.Close();
                }
                return students;
            }
        }
        #endregion

        #region Get Student By RollNumber
        public Student GetStudentByRollNumber(string rollNumber)
        {
            Student student = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                if (connection != null)
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("sp_GetStudentByRollNumber", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RollNumber", rollNumber);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        student = new Student
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            RollNumber = reader["RollNumber"].ToString(),
                            Name = reader["Name"].ToString(),
                            Maths = Convert.ToInt32(reader["Maths"]),
                            Physics = Convert.ToInt32(reader["Physics"]),
                            Chemistry = Convert.ToInt32(reader["Chemistry"]),
                            English = Convert.ToInt32(reader["English"]),
                            Programming = Convert.ToInt32(reader["Programming"]),
                            IsActive = Convert.ToBoolean(reader["IsActive"])
                        };
                    }
                }
                return student;
            }
        }
        #endregion

        #region Get Student By UserId
        public Student GetStudentByUserId(int userId)
        {
            Student student = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                if (connection != null)
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("sp_GetStudentByUserId", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        student = new Student
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            RollNumber = reader["RollNumber"].ToString(),
                            Name = reader["Name"].ToString(),
                            Maths = Convert.ToInt32(reader["Maths"]),
                            Physics = Convert.ToInt32(reader["Physics"]),
                            Chemistry = Convert.ToInt32(reader["Chemistry"]),
                            English = Convert.ToInt32(reader["English"]),
                            Programming = Convert.ToInt32(reader["Programming"]),
                            IsActive = Convert.ToBoolean(reader["IsActive"])
                        };
                    }
                }
                return student;
            }
        }
        #endregion

        #region Add Student
        public void AddStudent(Student student)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                if (connection != null)
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("sp_AddStudent", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@RollNumber", student.RollNumber);
                    cmd.Parameters.AddWithValue("@Name", student.Name);
                    cmd.Parameters.AddWithValue("@Maths", student.Maths);
                    cmd.Parameters.AddWithValue("@Physics", student.Physics);
                    cmd.Parameters.AddWithValue("@Chemistry", student.Chemistry);
                    cmd.Parameters.AddWithValue("@English", student.English);
                    cmd.Parameters.AddWithValue("@Programming", student.Programming);
                    cmd.Parameters.AddWithValue("@UserId", (object)student.UserId ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
        #endregion

        #region Update Student Marks
        public void UpdateStudentMarks(Student student)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                if (connection != null)
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("sp_UpdateStudentMarks", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@RollNumber", student.RollNumber);
                    cmd.Parameters.AddWithValue("@Maths", student.Maths);
                    cmd.Parameters.AddWithValue("@Physics", student.Physics);
                    cmd.Parameters.AddWithValue("@Chemistry", student.Chemistry);
                    cmd.Parameters.AddWithValue("@English", student.English);
                    cmd.Parameters.AddWithValue("@Programming", student.Programming);

                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
        #endregion

        #region Delete Student
        public void DeleteStudent(string rollNumber)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                if (connection != null)
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("sp_DeleteStudent", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RollNumber", rollNumber);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
        #endregion

        #region Get Next Roll Number
        public string GetNextRollNumber()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_GetNextRollNumber", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                return cmd.ExecuteScalar().ToString();
            }
        }
        #endregion
    }
}

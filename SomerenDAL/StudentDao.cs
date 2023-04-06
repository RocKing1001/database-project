using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using SomerenModel;
using System;

namespace SomerenDAL
{
    public class StudentDao : BaseDao
    {
        public List<Student> GetAllStudents()
        {
            string query = "SELECT uid, name, dob FROM Person JOIN Student ON Person.uid = Student.id";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));

        }
        public List<Student> GetStudentsFromActivities(string activity)
        {
            string query = "SELECT uid, name, dob FROM Participants P JOIN Student S on P.student_id = S.id JOIN Activity A on P.activity_id = A.activity_id JOIN Person P2 on S.id = P2.uid AND A.type = @activity";
            SqlParameter[] sqlParameters = {
                new SqlParameter("@activity", activity),
            };
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));

        }

        private List<Student> ReadTables(DataTable dataTable)
        {
            List<Student> students = new List<Student>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Console.WriteLine(dr["uid"]);
                Student student = new Student()
                {
                    Number = (int)dr["uid"],
                    Name = (string)dr["name"],
                    BirthDate = (DateTime)dr["dob"]
                };
                students.Add(student);
            }
            return students;
            
        }
    }
}
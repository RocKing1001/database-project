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

        public List<Student> GetStudentsWithoutActivities()
        {
            string query = "SELECT uid, name, dob FROM Person P JOIN Student S ON P.uid = S.id LEFT JOIN Participants P2 ON S.id = P2.student_id WHERE P2.activity_id IS NULL;";
            SqlParameter[] sqlParameters = {
            };
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        public void RemoveParticipatingStudent(string id) {
            string query = "DELETE FROM Participants WHERE student_id = @id";
            SqlParameter[] sqlParameters = {
                new SqlParameter("@id", id),
            };
            ExecuteEditQuery(query, sqlParameters);
        }
        public void AddParticipatingStudent(string id, string type) {
            string query = "INSERT INTO Participants (student_id, activity_id) VALUES (@id, (SELECT TOP 1 activity_id FROM Activity WHERE Type = @type))";
            SqlParameter[] sqlParameters = {
                new SqlParameter("@id", id),
                new SqlParameter("@type", type),
            };
            ExecuteEditQuery(query, sqlParameters);
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
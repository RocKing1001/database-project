using SomerenDAL;
using SomerenModel;
using System.Collections.Generic;

namespace SomerenService
{
    public class StudentService
    {
        private StudentDao studentdb;

        public StudentService()
        {
            studentdb = new StudentDao();
        }

        public List<Student> GetStudents()
        {
            List<Student> students = studentdb.GetAllStudents();
            return students;
        }

        public List<Student> GetStudentsWithActivities(string activity)
        {
            List<Student> students = studentdb.GetStudentsFromActivities(activity);
            return students;
        }
    }
}
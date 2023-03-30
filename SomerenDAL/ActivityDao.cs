using SomerenModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Activity = SomerenModel.Activity;

namespace SomerenDAL
{
    public class ActivityDao:BaseDao
    {
        public List<Activity> GetAllActivity()
        {
            string query = @"
            SELECT  activity_id,type,start_time,end_time,date
            FROM Activity 
            ORDER BY type";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));

        }
        private List<Activity> ReadTables(DataTable dataTable)
        {
            List<Activity> activities = new List<Activity>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Activity activity = new Activity()
                {
                    Id = (int)dr["activity_id"],
                    Type = dr["type"].ToString(),
                    StartTime = (DateTime)dr["start_time"],
                    EndTime = (DateTime)dr["end_time"],
                };
                activities.Add(activity);
            }

            return activities;
        }
        public void AddActivity(Activity activity)
        {


            string query = "INSERT INTO Activity (type, start_time, end_time) VALUES (@type, @start_time, @end_time);";
            SqlParameter[] sqlParameters = {
                
                new SqlParameter("@type", activity.Type),
                new SqlParameter("@start_time", activity.StartTime ),
                new SqlParameter("@end_time", activity.EndTime),
              
            };
            ExecuteEditQuery(query, sqlParameters);


        }

        public void DeleteActivity(Activity activity)
        {
            string query = ("DELETE FROM Activity WHERE activity_id = @Id;");
            SqlParameter[] sqlParameters =
            {    new SqlParameter("@Id", activity.Id)
            };
            ExecuteEditQuery(query, sqlParameters);
        }

        public void UpdateActivity(Activity activity)
        {
            string query = "UPDATE Activity SET type = @Newtype, start_time = @NewStart_time, end_time = @NewEnd_time  WHERE activity_id = @Id";
            SqlParameter[] sqlParameters =
            {

                new SqlParameter("@Newtype",activity.Type),
                new SqlParameter("@NewStart_time", activity.StartTime),
                new SqlParameter("@NewEnd_time", activity.EndTime),
                new SqlParameter("@Id", activity.Id)
            };

            ExecuteEditQuery(query, sqlParameters);
        }

    }
}

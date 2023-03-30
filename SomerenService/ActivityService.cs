using SomerenDAL;
using SomerenModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenService
{
    public class ActivityService
    {
        private ActivityDao activityDb;

        public ActivityService()
        {
            activityDb = new ActivityDao();
        }
        public List<Activity> GetActivities()
        {
            List<Activity> activities = activityDb.GetAllActivity();
            return activities;
        }
        public void InsertActivity(Activity activity)
        {

            activityDb.AddActivity(activity);

        }

        public void DeleteActivity(Activity activity)
        {
            activityDb.DeleteActivity(activity);
        }

        public void UpdateActivity(Activity activity)
        {
            activityDb.UpdateActivity(activity);
        }
    }
}

using SomerenDAL;
using SomerenModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenService
{
    public class RevenueReportService
    {
        private RevenueDao studentdb;

        public RevenueReportService()
        {
            studentdb = new RevenueDao();
        }

        public List<RevenueReport> GetRevenueReports()
        {
            List<RevenueReport> rooms = studentdb.GetAllReports();
            return rooms;
        }
        public List<RevenueReport> GetRevenueReportsDated(string dateStart, string dateEnd)
        {
            List<RevenueReport> rooms = studentdb.GetDateReports(dateStart, dateEnd);
            return rooms;
        }
        public (int, decimal) GetCustomers(string dateStart, string dateEnd)
        {
            return studentdb.GetCustomers(dateStart, dateEnd);
        }
    }
}

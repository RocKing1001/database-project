using SomerenModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenDAL
{
    public class RevenueDao : BaseDao
    {
        public List<RevenueReport> GetAllReports()
        {
            string query = "SELECT * FROM [DrinksLedger]";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        public List<RevenueReport> GetDateReports(string date, string dateEnd)
        {
            string query = $"SELECT * FROM [DrinksLedger] where date between @date and @dateEnd";
            SqlParameter[] sqlParameters = {
                new SqlParameter("@date", date),
                new SqlParameter("@dateEnd", dateEnd),
            };
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<RevenueReport> ReadTables(DataTable dataTable)
        {
            List<RevenueReport> rooms = new List<RevenueReport>();

            foreach (DataRow dr in dataTable.Rows)
            {
                RevenueReport revenue = new RevenueReport()
                {
                    SaleId = (int)dr["sale_id"],
                    PurchaserId = (int)dr["purchaser_id"],
                    DrinkId = (int)dr["drink_id"],
                    Date = ((DateTime)dr["date"]).ToString("dd-MM-yyyy"),
                };
                rooms.Add(revenue);
            }
            return rooms;
        }
    }
}

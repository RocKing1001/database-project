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
            string query = $"SELECT p.name AS person_name, d.type AS drink_name, dl.date, sale_id FROM DrinksLedger dl JOIN Person p ON p.uid = dl.purchaser_id JOIN Drinks d ON d.drink_id = dl.drink_id";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        public List<RevenueReport> GetDateReports(string date, string dateEnd)
        {
            string query = $"SELECT p.name AS person_name, d.type AS drink_name, dl.date, sale_id FROM DrinksLedger dl JOIN Person p ON p.uid = dl.purchaser_id JOIN Drinks d ON d.drink_id = dl.drink_id WHERE date between @date and @dateEnd";
            SqlParameter[] sqlParameters = {
                new SqlParameter("@date", date),
                new SqlParameter("@dateEnd", dateEnd),
            };
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        public (int, decimal) GetCustomers(string date, string dateEnd) {
            string query = $"SELECT price FROM DrinksLedger dl JOIN Student p ON p.id = dl.purchaser_id JOIN Drinks d ON d.drink_id = dl.drink_id WHERE date between @date and @dateEnd";
            SqlParameter[] sqlParameters = {
                new SqlParameter("@date", date),
                new SqlParameter("@dateEnd", dateEnd),
            };
            DataTable dt = ExecuteSelectQuery(query, sqlParameters);

            int max_customers = 0;
            decimal cost = 0;

            foreach (DataRow dr in dt.Rows) {
                max_customers += 1;
                cost += (decimal)dr["price"];
            }

            return (max_customers, cost);
        }

        private List<RevenueReport> ReadTables(DataTable dataTable)
        {
            List<RevenueReport> rooms = new List<RevenueReport>();

            foreach (DataRow dr in dataTable.Rows)
            {
                RevenueReport revenue = new RevenueReport()
                {
                    SaleId = (int)dr["sale_id"],
                    Purchaser = (string)dr["person_name"],
                    Drink = (string)dr["drink_name"],
                    Date = ((DateTime)dr["date"]).ToString("dd-MM-yyyy"),
                };
                rooms.Add(revenue);
            }
            return rooms;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomerenModel;

namespace SomerenDAL
{
    public  class DrinkDao:BaseDao
    {
        public List<Drink> GetAllDrinks()
        {
            string query = @"
            SELECT  drink_id,is_alcoholic,type, price, stock
            FROM Drinks 
            ORDER BY type";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));

        }

        private List<Drink> ReadTables(DataTable dataTable)
        {
            List<Drink> drinks = new List<Drink>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Drink drink = new Drink()
                {
                    Id = (int)dr["drink_id"],
                    IsAlcoholic = (bool)dr["is_alcoholic"],
                    Name = dr["type"].ToString(),
                    Price = (decimal)dr["price"],
                    Stock = (int)dr["stock"],
                };
                drinks.Add(drink);
            }

            return drinks;
        }
        public void AddDrinks(Drink drink)
        {

            string query = ("INSERT INTO Drinks (is_alcoholic,type, price, stock) VALUES (@is_alcoholic, @type, @price, @stock);" + "SELECT SCOPE_IDENTITY();");
            SqlParameter[] sqlParameters = {
               new SqlParameter("@is_alcoholic", drink.IsAlcoholic ? 1 : 0),
                new SqlParameter("@type",drink.Name),
                new SqlParameter("@price", drink.Price),
                new SqlParameter("@stock", drink.Stock)
            };
            ExecuteEditQuery(query, sqlParameters);


        }
        public void DeleteDrinks(Drink drink)
        {
            string query = ("DELETE FROM Drinks WHERE drink_id = @Id;");
            SqlParameter[] sqlParameters =
            {    new SqlParameter("@Id", drink.Id)
            };
            ExecuteEditQuery(query, sqlParameters);
        }
        public void UpdateDrinks(Drink drink)
        {
            string query = "UPDATE Drinks SET is_alcoholic = @is_alcoholic, Type = @Newtype, Stock = @NewStock, Price = @NewPrice WHERE drink_id = @Id";
            SqlParameter[] sqlParameters =
            {

                  new SqlParameter("@is_alcoholic", drink.IsAlcoholic),
                   new SqlParameter("@Newtype", drink.Name),
                   new SqlParameter("@NewStock", drink.Stock),
                        new SqlParameter("@NewPrice", drink.Price),
                        new SqlParameter("@Id", drink.Id)
            };

            ExecuteEditQuery(query, sqlParameters);



        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomerenDAL;
using SomerenModel;

namespace SomerenService
{
    public class DrinkService
    {
        private DrinkDao drinkdb;


        public DrinkService()
        {
            drinkdb = new DrinkDao();
        }

        public List<Drink> GetDrinks()
        {
            List<Drink> drinks = drinkdb.GetAllDrinks();
            return drinks;
        }
        public void InsertDrinks(Drink drink)
        {

            drinkdb.AddDrinks(drink);

        }
        public void DeleteDrinks(Drink drink)
        {
            drinkdb.DeleteDrinks(drink);
        }
        public void UpdateDrinks(Drink drink)
        {
            drinkdb.UpdateDrinks(drink);
        }
    }
}

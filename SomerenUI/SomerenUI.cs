using SomerenService;
using SomerenModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System;
using System.Drawing;
using SomerenDAL;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Linq;
using System.Globalization;

namespace SomerenUI
{
    public partial class SomerenUI : Form
    {
        public SomerenUI()
        {
            InitializeComponent();
            HidePanels();
            pnlDashboard.Show();
            dateTimePickerRR.CustomFormat = "dd/MM/yyyy";
            dateTimePickerRR.Format = DateTimePickerFormat.Custom;
            dateTimePickerRR.MaxDate = DateTime.Now;
            dateTimePickerEnd.CustomFormat = "dd/MM/yyyy";
            dateTimePickerEnd.Format = DateTimePickerFormat.Custom;
            dateTimePickerEnd.MaxDate = DateTime.Now;
        }

        private void HidePanels()
        {
            foreach (var pnl in Controls.OfType<Panel>())
            {
                pnl.Hide();
            }
        }

        private void ShowDashboardPanel()
        {
            // hide all other panels
            HidePanels();

            // show dashboard
            pnlDashboard.Show();
        }

        private void ShowStudentsPanel()
        {
            // hide all other panels
            HidePanels();

            // show students
            pnlStudents.Show();

            try
            {
                // get and display all students
                List<Student> students = GetStudents();
                DisplayStudents(students);
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong while loading the students: " + e.Message);
            }
        }

        private void ShowRevenueReportPanel()
        {
            // hide all other panels
            HidePanels();

            // show students
            pnlRevenueReport.Show();

            try
            {
                // get and display all students
                List<RevenueReport> reports = new RevenueReportService().GetRevenueReports();
                DisplayRevenue(reports);
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong while loading the reports: " + e.Message);
            }
        }

        private void ShowRoomsPanel()
        {
            // hide all other panels
            HidePanels();

            // show students
            pnlRooms.Show();

            try
            {
                // get and display all students
                List<Room> rooms = GetRooms();
                DisplayRooms(rooms);
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong while loading the Rooms: " + e.Message);
            }
        }
        private void ShowActivityPanel()
        {
            HidePanels();
            drinkpanel.Show();
            activitypanel.Show();

            try
            {
                // get and display all students
                List<Activity> activities = GetActivities();
                DisplayActivities(activities);

            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong while loading the Rooms: " + e.Message);
            }


        }

        private List<Student> GetStudents()
        {
            StudentService studentService = new StudentService();
            List<Student> students = studentService.GetStudents();
            return students;
        }

        private List<Room> GetRooms()
        {
            RoomService roomService = new RoomService();
            List<Room> rooms = roomService.Getrooms();
            return rooms;
        }
        private List<Activity> GetActivities()
        {
            ActivityService activityService = new ActivityService();
            List<Activity> activities = activityService.GetActivities();
            return activities;
        }

        private void DisplayRevenue(List<RevenueReport> reports)
        {

            listRevenueReport.Clear();
            listRevenueReport.Columns.Add("Sale ID", 100);
            listRevenueReport.Columns.Add("Purchased by", 100);
            listRevenueReport.Columns.Add("Drink ID", 150);
            listRevenueReport.Columns.Add("Date", 150);

            foreach (RevenueReport report in reports)
            {
                ListViewItem list = new ListViewItem(report.SaleId.ToString());

                list.SubItems.Add(report.Purchaser.ToString());
                list.SubItems.Add(report.Drink.ToString());
                list.SubItems.Add(report.Date.ToString());

                list.Tag = report;   // link student object to listview item
                listRevenueReport.Items.Add(list);

            }
            listRevenueReport.View = View.Details;

            var (max_customers, cost) = new RevenueReportService().GetCustomers("1970-01-01", DateTime.Now.ToString("yyyy-MM-dd"));

            turnoverOut.Text = "€" + cost.ToString(".00");
            customersOut.Text = max_customers.ToString();

        }

        private void DisplayStudents(List<Student> students)
        {
            // clear the listview before filling it
            listViewStudents.Clear();
            listViewStudents.Columns.Add("StudentID", 40);
            listViewStudents.Columns.Add("Name", 100);
            listViewStudents.Columns.Add("Date of birth", 150);

            foreach (Student student in students)
            {
                ListViewItem list = new ListViewItem(student.Number.ToString());

                list.SubItems.Add(student.Name.ToString());
                list.SubItems.Add(student.BirthDate.ToString());

                list.Tag = student;   // link student object to listview item
                listViewStudents.Items.Add(list);

            }

        }
        private void DisplayDrinks(List<Drink> drinks)
        {
            //clear the listview before filling it
            listViewDrinks.Clear();

            listViewDrinks.Columns.Add("Name", 50);
            listViewDrinks.Columns.Add("Type", 50);
            listViewDrinks.Columns.Add("Price", 50);
            listViewDrinks.Columns.Add("Stock", 50);
            listViewDrinks.Columns.Add("Stock Status", 50);


            foreach (Drink drink in drinks)
            {
                ListViewItem list = new ListViewItem(drink.Name.ToString());

                list.SubItems.Add(drink.IsAlcoholic ? "Alcoholic" : "Non-alcoholic");
                list.SubItems.Add(drink.Price.ToString());
                list.SubItems.Add(drink.Stock.ToString());
                list.SubItems.Add(drink.Stock < 10 ? "Stock nearly depleted" : "Stock sufficient");

                list.Tag = drink;   // link drink object to listview item
                listViewDrinks.Items.Add(list);

            }
            listViewDrinks.Columns[0].Width = 100;
            listViewDrinks.Columns[1].Width = 100;
            listViewDrinks.Columns[2].Width = 100;
            listViewDrinks.Columns[3].Width = 100;
            listViewDrinks.Columns[4].Width = 150;



        }
        private void DisplayActivities(List<Activity> activities)
        {
            listViewActivity.Clear();



            listViewActivity.Columns.Add("Activity", 50);
            listViewActivity.Columns.Add("Start Time", 100);
            listViewActivity.Columns.Add("End Time", 100);



            foreach (Activity activity in activities)
            {
                ListViewItem list = new ListViewItem(activity.Type.ToString());


                list.SubItems.Add(activity.StartTime.ToString());
                list.SubItems.Add(activity.EndTime.ToString());

                list.Tag = activity;   // link activities object to listview item
                listViewActivity.Items.Add(list);

            }
            listViewActivity.Columns[0].Width = 150;
            listViewActivity.Columns[1].Width = 150;
            listViewActivity.Columns[2].Width = 150;






        }
        private List<Drink> GetDrinks()
        {

            DrinkService drinkService = new DrinkService();
            List<Drink> drinks = drinkService.GetDrinks();

            return drinks;
        }
        private void ShowDrinksPanel()
        {
            HidePanels();
            activitypanel.Hide();



            drinkpanel.Show();

            try
            {
                // get and display all drinks
                List<Drink> drinks = GetDrinks();
                DisplayDrinks(drinks);
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong while loading the Drinks: " + e.Message);
            }

        }

        private void DisplayRooms(List<Room> rooms)
        {
            listViewRooms.Items.Clear();
            listViewRooms.Columns.Add("", 0); // dont ask me
            listViewRooms.Columns.Add("Number");
            listViewRooms.Columns.Add("Capacity");

            foreach (Room room in rooms)
            {
                ListViewItem li = new ListViewItem();
                li.SubItems.Add(room.Id.ToString());
                li.SubItems.Add(room.Capacity.ToString());
                listViewRooms.Items.Add(li);
            }
            listViewRooms.View = View.Details;
        }

        private void dashboardToolStripMenuItem1_Click(object sender, System.EventArgs e)
        {
            ShowDashboardPanel();
        }

        private void exitToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        private void studentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowStudentsPanel();
        }


        private void roomsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowRoomsPanel();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowRevenueReportPanel();
        }

        private void revenueReportGet_Click(object sender, EventArgs e)
        {
            var dateStart = dateTimePickerRR.Value.ToString("yyyy-MM-dd");
            var dateEnd = dateTimePickerEnd.Value.ToString("yyyy-MM-dd");
            var reports = new RevenueReportService().GetRevenueReportsDated(dateStart, dateEnd);
            DisplayRevenue(reports);
            var (max_customers, cost) = new RevenueReportService().GetCustomers(dateStart, dateEnd);

            turnoverOut.Text = cost.ToString();
            customersOut.Text = max_customers.ToString();

        }

        private void dateTimePickerRR_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerEnd.MinDate = dateTimePickerRR.Value;
        }

        private void DrinkStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowDrinksPanel();
        }

        private void addrinksbtn_Click(object sender, EventArgs e)
        {
            if (new[] { drinkNameinputBox, stockamoutinputtxt, priceinputtxtlbl }.Any(textBox => string.IsNullOrWhiteSpace(textBox.Text)))
            {
                MessageBox.Show("Please fill the required fields.");
                return;
            }

            DrinkService drinkService = new DrinkService();
            Drink selectDrink = new Drink();

            try
            {

                // update the selectDrink properties with the new values
                selectDrink.Name = drinkNameinputBox.Text;
                selectDrink.Stock = int.Parse(stockamoutinputtxt.Text);
                selectDrink.Price = decimal.Parse(priceinputtxtlbl.Text);
                if (alcoholicrdiobtn.Checked || nonalcoholic.Checked)
                {
                    if (alcoholicrdiobtn.Checked)
                        selectDrink.IsAlcoholic = true;
                    else
                        selectDrink.IsAlcoholic = false;
                }
                else
                    throw new Exception("You need to select type");
                // insert selectDrink instance into the database
                drinkService.InsertDrinks(selectDrink);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong while adding the drinks. Error message: " + ex.Message);
            }

            List<Drink> drinks = GetDrinks();
            DisplayDrinks(drinks);
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            if (listViewDrinks.SelectedItems.Count == 0)
            {
                return;
            }
            DialogResult result = MessageBox.Show("Do you want to delete this selected drink", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                return;
            }
            DrinkService drinkService = new DrinkService();
            try
            {


                foreach (ListViewItem listViewItem in listViewDrinks.SelectedItems)
                {
                    Drink selectedDrink = (Drink)listViewItem.Tag;
                    drinkService.DeleteDrinks(selectedDrink);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong while deleting the drinks. Error message: " + ex.Message);
            }
            List<Drink> drinks = GetDrinks();



            DisplayDrinks(drinks);
        }

        private void Editbtn_Click(object sender, EventArgs e)
        {
            if (listViewDrinks.SelectedItems.Count == 0)
                return;

            Drink drink = (Drink)listViewDrinks.SelectedItems[0].Tag;

            if (new[] { drinkNameinputBox, stockamoutinputtxt, priceinputtxtlbl }.Any(textBox => string.IsNullOrWhiteSpace(textBox.Text)))
            {
                MessageBox.Show("Please fill the required fields.");
                return;
            }

            else
            {
                drink.Name = drinkNameinputBox.Text;
                drink.Stock = int.Parse(stockamoutinputtxt.Text);
                drink.Price = decimal.Parse(priceinputtxtlbl.Text);

            }
            DrinkService drinkService = new DrinkService();
            try
            {
                if (alcoholicrdiobtn.Checked || nonalcoholic.Checked)
                {
                    if (alcoholicrdiobtn.Checked)
                        drink.IsAlcoholic = true;
                    else
                        drink.IsAlcoholic = false;
                }
                else
                    throw new Exception("You need to select type");
                drinkService.UpdateDrinks(drink);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong while updating the drinks. Error message: " + ex.Message);
            }

            List<Drink> drinks = GetDrinks();
            DisplayDrinks(drinks);
        }

        private void listViewDrinks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewDrinks.SelectedItems.Count == 0)
            {
                return;
            }
            Drink drink = (Drink)listViewDrinks.SelectedItems[0].Tag;
            drinkNameinputBox.Text = drink.Name;
            stockamoutinputtxt.Text = drink.Stock.ToString();

            priceinputtxtlbl.Text = drink.Price.ToString();
        }

        private void activitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowActivityPanel();
        }

        private void Addactivitybtn_Click(object sender, EventArgs e)
        {

            ActivityService activityService = new ActivityService();

            if (activityType.Text == "")
            {
                MessageBox.Show("Please add an activity type.");
                return;
            }

            if (dateTimeStart.Value == dateTimeStart.MinDate || dateTimeEnd.Value == dateTimeEnd.MinDate)
            {
                MessageBox.Show("Please select both start time and end time.");
                return;
            }


            try
            {
                Activity selectActivity = gettingDataOfActivity();

                // Insert the new activity into the database
                activityService.InsertActivity(selectActivity);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong while adding the activities. Error message: " + ex.Message);
            }

            List<Activity> activities = GetActivities();
            DisplayActivities(activities);

        }

        private Activity gettingDataOfActivity()
        {
            Activity activity = new Activity(); 
            activity.Type = activityType.Text;
            activity.StartTime = (DateTime)(dateTimeStart.Value);
            activity.EndTime = (DateTime)(dateTimeEnd.Value);

            return activity;
        }

        private void Deleteactivitybtn_Click(object sender, EventArgs e)
        {
            if (listViewActivity.SelectedItems.Count == 0)
            {
                return;
            }
            DialogResult result = MessageBox.Show("Do you want to delete this selected activity", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                return;
            }

            ActivityService activityService = new ActivityService();
            try
            {


                foreach (ListViewItem listViewItem in listViewActivity.SelectedItems)
                {

                    Activity selectedActivity = (Activity)listViewItem.Tag;
                    activityService.DeleteActivity(selectedActivity);


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong while deleting the activity. Error message: " + ex.Message);
            }

            List<Activity> activities = GetActivities();

            DisplayActivities(activities);

        }

        private void listViewActivity_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listViewActivity.SelectedItems.Count == 0)
            {
                return;
            }

            Activity activity = (Activity)listViewActivity.SelectedItems[0].Tag;
            activityType.Text = activity.Type;
            dateTimeStart.Text = activity.StartTime.ToString();
            dateTimeEnd.Text = activity.EndTime.ToString();





        }

        private void updateActivitybtn_Click(object sender, EventArgs e)
        {
            if (listViewActivity.SelectedItems.Count == 0)
                return;

            if (activityType.Text == "" || dateTimeStart.Value == dateTimeStart.MinDate || dateTimeEnd.Value == dateTimeEnd.MinDate)
            {
                MessageBox.Show("Please fill in all the required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Activity activity = (Activity)listViewActivity.SelectedItems[0].Tag;

            // Update the activity type if it has changed
            if (activityType.Text != activity.Type)
            {
                activity.Type = activityType.Text;
            }

            // Update the start time if it has changed
            if (dateTimeStart.Value != activity.StartTime)
            {
                activity.StartTime = dateTimeStart.Value;
            }

            // Update the end time if it has changed
            if (dateTimeEnd.Value != activity.EndTime)
            {
                activity.EndTime = dateTimeEnd.Value;
            }

            ActivityService activityService = new ActivityService();
            try
            {
                activityService.UpdateActivity(activity);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong while updating the activities. Error message: " + ex.Message);
            }

            List<Activity> activities = GetActivities();
            DisplayActivities(activities);
        }
    }
}
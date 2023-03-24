using SomerenService;
using SomerenModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System;
using SomerenDAL;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Linq;

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

                list.SubItems.Add(report.PurchaserId.ToString());
                list.SubItems.Add(report.DrinkId.ToString());
                list.SubItems.Add(report.Date.ToString());

                list.Tag = report;   // link student object to listview item
                listRevenueReport.Items.Add(list);

            }
            listRevenueReport.View = View.Details;
        }

        private void DisplayStudents(List<Student> students)
        {
            // clear the listview before filling it
            listViewStudents.Clear();
            listViewStudents.Columns.Add("StudentID", 100);
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
            listViewStudents.Columns[0].Width = 200;
            listViewStudents.Columns[1].Width = 200;
            listViewStudents.Columns[2].Width = 200;
        }

        private void DisplayRooms(List<Room> rooms)
        {
            // clear the listview before filling it
            listViewRooms.Clear();

            listViewRooms.Columns.Add("Number");
            listViewRooms.Columns.Add("Capacity");

            foreach (Room room in rooms)
            {
                ListViewItem li = new ListViewItem();
                li.SubItems.Add(room.Id.ToString());
                li.SubItems.Add(room.Capacity.ToString());
                li.Tag = room;   // link student object to listview item
                listViewRooms.Items.Add(li);
            }

            listViewRooms.Columns[0].Width = 50;
            listViewRooms.Columns[1].Width = 50;
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

        }
    }
}
using SomerenService;
using SomerenModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System;
using SomerenDAL;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SomerenUI
{
    public partial class SomerenUI : Form
    {
        public SomerenUI()
        {
            InitializeComponent();
        }

        private void ShowDashboardPanel()
        {
            // hide all other panels
            pnlStudents.Hide();
            pnlRooms.Hide();

            // show dashboard
            pnlDashboard.Show();
        }

        private void ShowStudentsPanel()
        {
            // hide all other panels
            pnlDashboard.Hide();
            pnlRooms.Hide();

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

        private void ShowRoomsPanel()
        {
            // hide all other panels
            pnlDashboard.Hide();
            pnlStudents.Hide();

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

    }
}
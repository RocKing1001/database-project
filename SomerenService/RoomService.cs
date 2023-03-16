using SomerenDAL;
using SomerenModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenService
{
    public class RoomService
    {
        private RoomDao studentdb;

        public RoomService()
        {
            studentdb = new RoomDao();
        }

        public List<Room> Getrooms()
        {
            List<Room> rooms = studentdb.GetAllStudents();
            return rooms;
        }
    }
}

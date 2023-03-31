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
    public class RoomDao : BaseDao
    {
        public List<Room> GetAllRooms()
        {
            //string query = "SELECT dorm_id, max_occupants FROM [DormRoom]";
            string query = "SELECT Rooms.room_id, IIF(DormRoom.dorm_id IS NOT NULL, DormRoom.max_occupants, 1) AS max_capacity, IIF(DormRoom.dorm_id IS NOT NULL, 'dorm', 'single') AS room_type FROM Rooms LEFT JOIN DormRoom ON Rooms.room_id = DormRoom.dorm_id LEFT JOIN SingleRoom ON Rooms.room_id = SingleRoom.single_room_id;";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Room> ReadTables(DataTable dataTable)
        {
            List<Room> rooms = new List<Room>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Room room = new Room()
                {
                    Id = (int)dr["room_id"],
                    Capacity = (int)dr["max_capacity"],
		    Type = (string)dr["room_type"]
                };
                rooms.Add(room);
            }
            return rooms;
        }
    }
}

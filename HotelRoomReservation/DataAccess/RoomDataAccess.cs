using HotelRoomReservation.Helpers;
using HotelRoomReservation.Models;
using System.Data.SqlClient;

namespace HotelRoomReservation.DataAccess
{
    internal class RoomDataAccess
    {
        public string ErrorMessage { get; set; }
        public List<RoomDataModel> GetAll()
        {
            try
            {
                ErrorMessage = String.Empty;
                ErrorMessage = "";
                List<RoomDataModel> Rooms = new List<RoomDataModel>();
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = "Select RoomId,RoomNumber,RoomType,Price,Availability from Room";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RoomDataModel room = new RoomDataModel();
                                room.RoomId = reader.GetInt32(0);
                                room.RoomNumber = reader.GetInt32(1);
                                room.RoomType = reader.GetString(2);
                                room.Price = reader.GetInt32(3);
                                room.Availability = reader.GetString(4);

                                Rooms.Add(room);
                            }
                        }
                    }
                }
                return Rooms;

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        //Get Room by Id
        public RoomDataModel GetRoomById(int id)
        {
            try
            {
                ErrorMessage = String.Empty;
                ErrorMessage = "";
                RoomDataModel room = null;
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"Select RoomId,RoomNumber,RoomType,Price,Availability from Room where RoomId={id}";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read() == true)
                            {
                                room = new RoomDataModel();
                                room.RoomId = reader.GetInt32(0);
                                room.RoomNumber = reader.GetInt32(1);
                                room.RoomType = reader.GetString(2);
                                room.Price = reader.GetInt32(3);
                                room.Availability = reader.GetString(4);

                            }
                        }
                    }
                }
                return room;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }

        }

        public List<RoomDataModel> GetRoomsByName(string name)
        {
            try
            {
                List<RoomDataModel> rooms = new List<RoomDataModel>();
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"Select RoomId,RoomNumber,RoomType,Price,Availability from Room where RoomNumber like '%{rooms}%' ";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                RoomDataModel room = new RoomDataModel();
                                room.RoomId = reader.GetInt32(0);
                                room.RoomNumber = reader.GetInt32(1);
                                room.RoomType = reader.GetString(2);
                                room.Price = reader.GetInt32(3);
                                room.Availability = reader.GetString(4);

                                rooms.Add(room);
                            }
                        }
                    }
                }

                return rooms;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public RoomDataModel Insert(RoomDataModel newRoom)
        {
            try
            {
                ErrorMessage = String.Empty;
                ErrorMessage = "";
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"INSERT INTO dbo.Room(RoomNumber,RoomType,Price,Availability) VALUES({newRoom.RoomNumber},'{newRoom.RoomType}',{newRoom.Price},'{newRoom.Availability}');SELECT SCOPE_IDENTITY();";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int idInserted = Convert.ToInt32(cmd.ExecuteScalar());
                        if (idInserted > 0)
                        {
                            newRoom.RoomId = idInserted;
                            return newRoom;
                        }
                    }

                }
                return null;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public RoomDataModel Update(RoomDataModel updRoom)
        {
            try
            {
                ErrorMessage = String.Empty;
                ErrorMessage = "";
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"UPDATE dbo.Room SET RoomNumber = {updRoom.RoomNumber}, RoomType= '{updRoom.RoomType}',Price={updRoom.Price},Availability ='{updRoom.Availability}' Where RoomId = {updRoom.RoomId} ";
                    

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int numOfRows = cmd.ExecuteNonQuery();
                        if (numOfRows > 0)
                        {
                            return updRoom;
                        }
                    }
                }
                return updRoom;

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
    }
}
using System.Data.SqlClient;
using HotelRoomReservation.Models;
using HotelRoomReservation.Helpers;

namespace HotelRoomReservation.DataAccess
{
    public class DashBoardDataAccess
    {
        public string ErrorMessage { get; private set; }
        public DashBoardDataModel GetAll()
        {
            try
            {

                ErrorMessage = String.Empty;
                ErrorMessage = "";
                var d = new DashBoardDataModel();
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = "select count(*) as RoomCount from Room";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        d.RoomCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    sqlStmt = "select count(*) as CustomerCount from Customer";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        d.CustomerCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    sqlStmt = "select count(*) as ReservationCount from Reservation";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        d.ReservationCount = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    

                }

                return d;


            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }

        }

    }
}



using System.Data.SqlClient;

namespace HotelRoomReservation.Helpers
{
    internal class DataBase
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = "Data Source=ENWIN-534\\SQLEXPRESS;Initial Catalog=HotelRoomReservation;Integrated Security=True;";
            return new SqlConnection(connectionString);
        }

    }
}

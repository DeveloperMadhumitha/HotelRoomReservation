using HotelRoomReservation.Helpers;
using HotelRoomReservation.Models;
using System.Data.SqlClient;

namespace HotelRoomReservation.DataAccess
{
    internal class ReservationDataAccess
    {
        public string ErrorMessage { get; set; }
        public List<ReservationDataModel> GetAll()
        {
            try
            {
                ErrorMessage = String.Empty;
                ErrorMessage = "";
                List<ReservationDataModel> Reservations = new List<ReservationDataModel>();
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();

                    var sqlStmt = "SELECT RE.ReservationId as ReservationId,R.RoomId AS RoomId ,C.CustomerId AS CustomerId,RE.DateIn,RE.DateOut,RE.Cost,RE.PaymentType,RE.Status " +
                                  "FROM[dbo].[Reservation] AS RE " +
                                  "INNER JOIN[dbo].Room AS R ON R.RoomId = RE.RoomId " +
                                  "INNER JOIN[dbo].Customer AS C ON C.CustomerId = RE.CustomerId " +
                                  "ORDER BY ReservationId ";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {



                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ReservationDataModel reservation = new ReservationDataModel();
                                reservation.ReservationId = reader.GetInt32(0);
                                reservation.RoomId = reader.GetInt32(1);
                                reservation.CustomerId = reader.GetInt32(2);
                                reservation.DateIn = reader.GetDateTime(3);
                                reservation.DateOut = reader.GetDateTime(4);
                                reservation.Cost = reader.GetInt32(5);
                                reservation.PaymentType = reader.GetString(6);
                                reservation.Status = reader.GetString(7);
                                Reservations.Add(reservation);
                            }
                        }

                    }
                }
                return Reservations;

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        //Get Reservation by Id
        public ReservationDataModel GetReservationById(int id)
        {
            try
            {
                ErrorMessage = String.Empty;
                ErrorMessage = "";
                ReservationDataModel reservation = null;
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"Select ReservationId,RoomId,CustomerId,DateIn,DateOut,Cost,PaymentType,Status from Reservation where Id={id}";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read() == true)
                            {
                                ReservationDataModel reservations = new ReservationDataModel();
                                reservation = new ReservationDataModel();
                                reservation.ReservationId = reader.GetInt32(0);
                                reservation.RoomId = reader.GetInt32(1);
                                reservation.CustomerId = reader.GetInt32(2);
                                reservation.DateIn = reader.GetDateTime(3);
                                reservation.DateOut = reader.GetDateTime(4);
                                reservation.Cost = reader.GetInt32(5);
                                reservation.PaymentType = reader.GetString(6);
                                reservation.Status = reader.GetString(7);

                            }
                        }
                    }
                }
                return reservation;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }

        }

        public List<ReservationDataModel> GetReservationsByName(string name)
        {
            try
            {
                List<ReservationDataModel> reservations = new List<ReservationDataModel>();
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"Select ReservationId,RoomId,CustomerId,DateIn,DateOut,Cost,PaymentType,Status from Reservation where ReservationId like '%{reservations}%' ";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                ReservationDataModel reservation = new ReservationDataModel();
                                reservation.ReservationId = reader.GetInt32(0);
                                reservation.RoomId = reader.GetInt32(1);
                                reservation.CustomerId = reader.GetInt32(2);
                                reservation.DateIn = reader.GetDateTime(3);
                                reservation.DateOut = reader.GetDateTime(4);
                                reservation.Cost = reader.GetInt32(5);
                                reservation.PaymentType = reader.GetString(6);
                                reservation.Status = reader.GetString(7);
                                reservations.Add(reservation);
                            }
                        }
                    }
                }

                return reservations;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public ReservationDataModel Insert(ReservationDataModel newReservation)
        {
            try
            {
                ErrorMessage = String.Empty;
                ErrorMessage = "";
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"INSERT INTO dbo.Reservation(RoomId,CustomerId,DateIn,DateOut,Cost,PaymentType,Status) VALUES({newReservation.RoomId},{newReservation.CustomerId},'{newReservation.DateIn.ToString("yyyy-MM-dd")}'," +
                        $"'{newReservation.DateOut.ToString("yyyy-MM-dd")}',{newReservation.Cost},'{newReservation.PaymentType}','{newReservation.Status}' );SELECT SCOPE_IDENTITY();";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int ReservationidInserted = Convert.ToInt32(cmd.ExecuteScalar());
                        if (ReservationidInserted > 0)
                        {
                            newReservation.ReservationId = ReservationidInserted;
                            return newReservation;
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
        public ReservationDataModel Update(ReservationDataModel updReservation)
        {
            try
            {
                ErrorMessage = String.Empty;
                ErrorMessage = "";
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = "Update dbo.Reservatoin SET " +
                        $"RoomId={updReservation.RoomId}," +
                        $"CustomerId= {updReservation.CustomerId}," +
                        $"DateIn= '{updReservation.DateIn}'," +
                        $"DateOut='{updReservation.DateOut}',"+
                        $"Cost={updReservation.Cost}," +
                        $"PaymentType='{updReservation.PaymentType}'," +
                        $"Status='{updReservation.Status}'" +
                        $"where ReservationId = {updReservation.ReservationId}";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int numOfRows = cmd.ExecuteNonQuery();
                        if (numOfRows > 0)
                        {
                            return updReservation;
                        }
                    }
                }
                return updReservation;

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
    }
}
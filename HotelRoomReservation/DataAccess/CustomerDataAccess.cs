using HotelRoomReservation.Helpers;
using HotelRoomReservation.Models;
using System.Data.SqlClient;

namespace HotelRoomReservation.DataAccess
{
    internal class CustomerDataAccess
    {
        public string ErrorMessage { get; set; }
        public List<CustomerDataModel> GetAll()
        {
            try
            {
                ErrorMessage = String.Empty;
                ErrorMessage = "";
                List<CustomerDataModel> customers = new List<CustomerDataModel>();
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = "Select CustomerId,CustomerName,Gender,Dob,MobileNumber,EmailId,City,State from Customer";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CustomerDataModel customer = new CustomerDataModel();
                                customer.CustomerId = reader.GetInt32(0);
                                customer.CustomerName = reader.GetString(1);
                                customer.Gender = reader.GetString(2);
                                customer.Dob = reader.GetDateTime(3);
                                customer.MobileNumber = reader.GetString(4);
                                customer.EmailId = reader.GetString(5);
                                customer.City = reader.GetString(6);
                                customer.State = reader.GetString(7);
                                
                                 customers.Add(customer);
                            }
                        }
                    }
                }
                return customers;

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        //Get Customer by Id
        public CustomerDataModel GetCustomerById(int id)
        {
            try
            {
                ErrorMessage = String.Empty;
                ErrorMessage = "";
                CustomerDataModel customer = null;
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"Select CustomerId,CustomerName,Gender,Dob,MobileNumber,EmailId,City,State from customer where Id={id}";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read() == true)
                            {
                                customer = new CustomerDataModel();
                                customer.CustomerId = reader.GetInt32(0);
                                customer.CustomerName = reader.GetString(1);
                                customer.Gender = reader.GetString(2);
                                customer.Dob = reader.GetDateTime(3);
                                customer.MobileNumber = reader.GetString(4);
                                customer.EmailId = reader.GetString(5);
                                customer.City = reader.GetString(6);
                                customer.State = reader.GetString(7);
                                

                            }
                        }
                    }
                }
                return customer;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }

        }

        public List<CustomerDataModel> GetCustomersByName(string name)
        {
            try
            {
                List<CustomerDataModel> customers = new List<CustomerDataModel>();
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"Select CustomerId, CustomerName,Gender,Dob,MobileNumber,EmailId,City,State from Customer where Name like '%{name}%' ";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                CustomerDataModel customer = new CustomerDataModel();
                                customer.CustomerId = reader.GetInt32(0);
                                customer.CustomerName = reader.GetString(1);
                                customer.Gender = reader.GetString(2);
                                customer.Dob = reader.GetDateTime(3);
                                customer.MobileNumber = reader.GetString(4);
                                customer.EmailId = reader.GetString(5);
                                customer.City = reader.GetString(6);
                                customer.State = reader.GetString(7);
                                

                                customers.Add(customer);
                            }
                        }
                    }
                }

                return customers;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public CustomerDataModel Insert(CustomerDataModel newCustomer)
        {
            try
            {
                ErrorMessage = String.Empty;
                ErrorMessage = "";
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"INSERT INTO dbo.Customer(CustomerName,Gender,Dob,MobileNumber,EmailId,City,State) VALUES('{newCustomer.CustomerName}','{newCustomer.Gender}','{newCustomer.Dob.ToString("yyyy-MM-dd")}','{newCustomer.MobileNumber}','{newCustomer.EmailId}','{newCustomer.City}','{newCustomer.State}');SELECT SCOPE_IDENTITY();";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int CustomeridInserted = Convert.ToInt32(cmd.ExecuteScalar());
                        if (CustomeridInserted > 0)
                        {
                            newCustomer.CustomerId = CustomeridInserted;
                            return newCustomer;
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
        public CustomerDataModel Update(CustomerDataModel updCustomer)
        {
            try
            {
                ErrorMessage = String.Empty;
                ErrorMessage = "";
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"UPDATE dbo.Customer SET CustomerName = '{updCustomer.CustomerName}', " +

                        $"Gender= '{updCustomer.Gender}'," +
                        $"Dob='{updCustomer.Dob.ToString("yyyy-MM-dd")}'," +
                        $"MobileNumber='{updCustomer.MobileNumber}', " +
                        $"EmailId= '{updCustomer.EmailId}'," +
                        $"City= '{updCustomer.City}'," +
                        $"State= '{updCustomer.State}'," +
                        

                       $"where CustomerId = '{updCustomer.CustomerId}'";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int numOfRows = cmd.ExecuteNonQuery();
                        if (numOfRows > 0)
                        {
                            return updCustomer;
                        }
                    }
                }
                return updCustomer;

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        //Delete Customer
        public int Delete(int id)
        {
            try
            {
                ErrorMessage = string.Empty;
                ErrorMessage = "";
                int numOfRows = 0;
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"DELETE FROM Customer Where Id = {id}";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        numOfRows = cmd.ExecuteNonQuery();

                    }
                }
                return numOfRows;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return 0;
            }
        }
    }
}



namespace HotelRoomReservation.Models
{
    public class CustomerDataModel
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Gender { get; set; }
        public DateTime Dob { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Id { get;  set; }


        //constructor
        public CustomerDataModel()
        {
            CustomerId = -1;
            CustomerName = "";
            Gender = "";
            Dob = DateTime.Now;
            MobileNumber = "";
            EmailId = "";
            City = "";
            State = "";
            
        }
    }
}

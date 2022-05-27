namespace HotelRoomReservation.Models
{
    public class RoomDataModel
    {
        public int RoomId { get; set; }
        public int RoomNumber { get; set; }
        public string RoomType { get; set; }
        public int Price { get; set; }
        
        public string Availability { get; set; }
        public int Id { get;  set; }

        //constructor
        public RoomDataModel()
        {
            RoomId = -1;
            RoomNumber = -1;
            RoomType = "";
            Price= -1;
            Availability = "";
            


        }
    }


}


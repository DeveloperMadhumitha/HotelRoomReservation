namespace HotelRoomReservation.Models
{
    public class ReservationDataModel
    {
        public int ReservationId { get; set; }
        public int RoomId { get; set; }
        public int CustomerId { get; set; }
        public DateTime DateIn { get; set; }
        public DateTime DateOut { get; set; }
        public int Cost  { get; set; }
        public string PaymentType { get; set; }
        public string Status { get; set; }

        //constructor
        public ReservationDataModel()
        {
            ReservationId = -1;
            RoomId = -1;
            CustomerId = -1;
            DateIn = DateTime.Now; 
            DateOut= DateTime.Now;
            Cost = 0;
            PaymentType = "";
            Status = "";
           


        }
    }


}


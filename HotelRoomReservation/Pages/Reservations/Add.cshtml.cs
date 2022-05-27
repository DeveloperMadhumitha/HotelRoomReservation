using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HotelRoomReservation.DataAccess;
using HotelRoomReservation.Models;
using System.ComponentModel.DataAnnotations;

namespace HotelRoomReservation.Pages.Reservations
{

    public class AddModel : PageModel
    {
        public int ReservationId { get; set; }
        public List<SelectListItem> CustomerList { get; set; }
        [BindProperty]
        [Display(Name = "CustomerId")]
       
        public int SelectedCustomerId{ get; set; }

        public List<SelectListItem>RoomList { get; set; }
        [BindProperty]
        [Display(Name = "RoomId")]
        [Required]
        public int SelectedRoomId { get; set; }
        
        [BindProperty]
        [Display(Name = "DateIn")]
        [Required]
        public DateTime DateIn { get; set; }

        [BindProperty]
        [Display(Name = "DateOut")]
        [Required]
        public DateTime DateOut { get; set; }

        [BindProperty]
        [Display(Name = "Cost")]
        [Required]

        public int Cost { get; set; }

        [BindProperty]
        [Display(Name = "PaymentType")]
        [Required]
        public string PaymentType { get; set; }


        [BindProperty]
        [Display(Name = "Status")]
        [Required]
        public string Status{ get; set; }


        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        
        public int RoomId { get; private set; }
        public object CustomerId { get; private set; }

        public  AddModel()
        {
            CustomerList = GetCustomers();
            RoomList = GetRooms();
            DateIn = DateTime.Now;
            DateOut = DateTime.Now;
            Cost = 0;
            PaymentType = "";
            Status = "";


            


        }
        public void OnGet()
        {

        }
        private List<SelectListItem> GetCustomers()
        {
            var customerDataAccess = new CustomerDataAccess();
            var customerList = customerDataAccess.GetAll();

            var customerSelectList = new List<SelectListItem>();
            foreach (var customer in customerList)
            {
                customerSelectList.Add(new SelectListItem
                {
                    Text = $"{customer.CustomerName}-{customer.City}",
                    Value = customer.CustomerId.ToString(),
                });
            }
            return customerSelectList;
        }
        private List<SelectListItem> GetRooms()
        {
            var roomDataAccess = new RoomDataAccess();
            var roomList = roomDataAccess.GetAll();

            var roomSelectList = new List<SelectListItem>();
            foreach (var room in roomList)
            {
                roomSelectList.Add(new SelectListItem
                {
                    Text = $"{room.RoomNumber}-{room.RoomType}",
                    Value = room.RoomId.ToString(),
                });
            }
            return roomSelectList;
        }

        public void OnPost()
        {

            //Reservations = GetReservations();

            if (!ModelState.IsValid)
            {
                ErrorMessage = "Error!! Add failed.Please try Again";
                return;
            }
            CustomerList = GetCustomers();
            RoomList = GetRooms();
            var reservationDataAccess = new ReservationDataAccess();
            var newreservation = new ReservationDataModel { ReservationId = ReservationId, RoomId = SelectedRoomId, CustomerId = SelectedCustomerId, DateIn = DateIn, DateOut = DateOut, Cost = Cost, PaymentType = PaymentType, Status = Status };
           
            var insertedReservation = reservationDataAccess.Insert(newreservation);
            if (insertedReservation != null)
            {
                SuccessMessage = $"successfully Inserted Reservation{insertedReservation.ReservationId}";
                ModelState.Clear();
            }
            else
            {
                ErrorMessage = $"Error!! Add failed.Please try Again - {reservationDataAccess.ErrorMessage}";
            }


        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HotelRoomReservation.DataAccess;
using HotelRoomReservation.Models;

namespace HotelRoomReservation.Pages.Reservations
{
    public class ListModel : PageModel
    {
        [BindProperty]
        public string SearchText { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public List<ReservationDataModel> Reservations { get; set; }
        public void OnGet()
        {
            var reservationDataAccess = new ReservationDataAccess();
            Reservations = reservationDataAccess.GetAll();

        }
        public ListModel()
        {
            Reservations = new List<ReservationDataModel>();
            ErrorMessage = "";
            SuccessMessage = "";
            SearchText = "";
        }
        public void OnPostSearch()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Enter the Valid Data";
            }
            if (string.IsNullOrEmpty(SearchText))
            {
                ErrorMessage = $"Please input more than 1 character";


            }
            DataAccess.ReservationDataAccess reservationDataAccess = new DataAccess.ReservationDataAccess();
            Reservations= reservationDataAccess.GetReservationsByName(SearchText);
            if (Reservations != null && (Reservations.Count > 0))
            {
                SuccessMessage = $"{Reservations.Count()} Reservations with '{SearchText}' found";

            }
            else
            {
                ErrorMessage = $"Records with '{SearchText}' Not Found";

            }
        }
        public void OnPostClear()
        {
            SearchText = "";
            ModelState.Clear();
            var reservationDataAccess = new DataAccess.ReservationDataAccess();
            Reservations = reservationDataAccess.GetAll();
            OnGet();
        }
    }
}


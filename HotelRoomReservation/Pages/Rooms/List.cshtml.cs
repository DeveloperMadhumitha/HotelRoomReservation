using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HotelRoomReservation.DataAccess;
using HotelRoomReservation.Models;

namespace HotelRoomReservation.Pages.Rooms
{
    public class ListModel : PageModel
    {
        [BindProperty]
        public string SearchText { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public List<RoomDataModel> Rooms { get; set; }
        public void OnGet()
        {
            var roomDataAccess = new RoomDataAccess();
            Rooms = roomDataAccess.GetAll();

        }
        public ListModel()
        {
            Rooms = new List<RoomDataModel>();
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
            DataAccess.RoomDataAccess roomDataAccess = new DataAccess.RoomDataAccess();
            Rooms = roomDataAccess.GetRoomsByName(SearchText);
            if (Rooms != null && (Rooms.Count > 0))
            {
                SuccessMessage = $"{Rooms.Count()} Rooms with '{SearchText}' found";

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
            var roomDataAccess = new DataAccess.RoomDataAccess();
            Rooms = roomDataAccess.GetAll();
            OnGet();
        }
    }
}



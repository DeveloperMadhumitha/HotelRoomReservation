using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HotelRoomReservation.DataAccess;
using HotelRoomReservation.Models;
using System.ComponentModel.DataAnnotations;

namespace HotelRoomReservation.Pages.Rooms
{

    public class AddModel : PageModel
    {
        [BindProperty]
        [Display(Name = "RoomNumber")]
        [Required]
        public int RoomNumber { get; set; }
        [BindProperty]
        [Display(Name = "RoomType")]
        [Required]
        public string RoomType { get; set; }
        //gender
        [BindProperty]
        [Display(Name = "Price")]
        [Required]
        public int Price { get; set; }
        


       
        
        [BindProperty]
        [Display(Name = "Availability")]
        [Required]
        public string Availability { get; set; }
        
       
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

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
       
        
       
        public void OnGet()
        {
            RoomNumber = -1;
            RoomType = "";
            Price = -1;
            Availability = "";
           
            


        }
        public void OnPost()
        {
           
            //Rooms = GetRooms();
           
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Error!! Add failed.Please try Again";
                return;
            }
            var roomDataAccess = new RoomDataAccess();
            var newroom = new RoomDataModel
            {
               RoomNumber= RoomNumber,
               RoomType = RoomType,
               Price = Price,
               Availability=Availability,
            };
            var insertedRoom = roomDataAccess.Insert(newroom);
            if (insertedRoom != null)
            {
                SuccessMessage = $"successfully Inserted Room{insertedRoom.RoomId}";
                ModelState.Clear();
            }
            else
            {
                ErrorMessage = $"Error!! Add failed.Please try Again - {roomDataAccess.ErrorMessage}";
            }


        }
    }
}

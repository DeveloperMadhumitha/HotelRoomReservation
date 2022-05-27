using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HotelRoomReservation.DataAccess;
using HotelRoomReservation.Models;
using System.ComponentModel.DataAnnotations;

namespace HotelRoomReservation.Pages.Rooms
{
    public class EditModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int RoomId { get; set; }
        [BindProperty]
        [Display(Name = "RoomNumber")]
        [Required]
        public int RoomNumber{ get; set; }

        [BindProperty]
        [Display(Name = "RoomType")]
        [Required]
        public string RoomType { get; set; }
       
        

        [BindProperty]
        [Display(Name = "Price")]
        [Required]
        public int Price{ get; set; }
        
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
        
        
        public void OnGet(int id)
        {
            RoomId = id;
            if (RoomId <  0)
            {
                ErrorMessage = "Invalid RoomId";
                return;
            }
            var roomDataAccess = new DataAccess.RoomDataAccess();
            var s = roomDataAccess.GetRoomById(id);
            if (s != null)
            {
                RoomNumber = s.RoomNumber;
                RoomType = s.RoomType;
                Price = s.Price;
                Availability = s.Availability;
               

            }
            else
            {

                ErrorMessage = "No Records found with RoomId";
            }

        }
        public void OnPost()
        {
            //Rooms = GetRooms();
           
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Error!! Update failed.Please try Again";
                return;
            }
            //update
            var roomDataAccess = new RoomDataAccess();
            var rooToUpdate = new RoomDataModel
            {
                RoomId = RoomId,
                RoomNumber = RoomNumber,
                RoomType = RoomType,
                Price = Price,
                Availability = Availability,
               
            };
            var updatedRoom = roomDataAccess.Update(rooToUpdate);
            if (updatedRoom != null)
            {
                SuccessMessage = $"Room {updatedRoom.RoomId} updated Successfully";
                Response.Headers.Add("Refresh", "3;URL=/Rooms/List");
            }
            else
            {
                ErrorMessage = $"Error..!!!  Updating Room Details. -'{roomDataAccess.ErrorMessage}'";
            }

        }
    }
}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HotelRoomReservation.DataAccess;

namespace HotelRoomReservation.Pages
{
    public class IndexModel : PageModel
    {

        public int RoomCount { get; set; }
        public int CustomerCount { get; set; }
        public int ReservationCount { get; set; }
        public int CompletedReservationCount { get; set; }



        public string ErrorMessage { get; set; }

        [FromQuery(Name = "action")]
        public string Action { get; set; }
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            RoomCount = 0;
            CustomerCount = 0;
            ReservationCount = 0;
            ErrorMessage = "";
        }

        public void OnGet()
        {
            if (!String.IsNullOrEmpty(Action) && Action.ToLower() == "logout")
            {
                Logout();
                return;
            }
            var dashboardData = new DashBoardDataAccess();
            var dashboard = dashboardData.GetAll();
            if (dashboard != null)
            {
                RoomCount = dashboard.RoomCount;
                CustomerCount = dashboard.CustomerCount;
                ReservationCount = dashboard.ReservationCount;
                
            }
            else
            {
                ErrorMessage = $"No Dashboard Data Available - {dashboardData.ErrorMessage}";
            }
        }

        public void OnPost()
        {
            Logout();
        }
        private void Logout()
        {
            HttpContext.SignOutAsync();
            Response.Redirect("/Index");
        }
    }
}



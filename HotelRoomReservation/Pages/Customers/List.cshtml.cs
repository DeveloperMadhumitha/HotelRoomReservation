using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HotelRoomReservation.DataAccess;
using HotelRoomReservation.Models;

namespace HotelRoomReservation.Pages.Customers
{
    public class ListModel : PageModel
    {
        [BindProperty]
        public string SearchText { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public List<CustomerDataModel> Customers { get; set; }
        public void OnGet()
        {
            var customerDataAccess = new CustomerDataAccess();
            Customers = customerDataAccess.GetAll();

        }

        public ListModel()
        {
            Customers = new List<CustomerDataModel>();
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
            DataAccess.CustomerDataAccess customerDataAccess = new DataAccess.CustomerDataAccess();
            Customers = customerDataAccess.GetCustomersByName(SearchText);
            if (Customers != null && (Customers.Count > 0))
            {
                SuccessMessage = $"{Customers.Count()} Cutomers with '{SearchText}' found";

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
            var customerDataAccess = new DataAccess.CustomerDataAccess();
            Customers = customerDataAccess.GetAll();
            OnGet();
        }
    }
}
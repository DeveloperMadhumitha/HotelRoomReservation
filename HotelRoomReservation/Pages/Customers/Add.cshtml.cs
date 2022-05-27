using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HotelRoomReservation.DataAccess;
using HotelRoomReservation.Models;
using System.ComponentModel.DataAnnotations;

namespace HotelRoomReservation.Pages.Customers
{

    public class AddModel : PageModel
    {
        [BindProperty]
        [Display(Name = "CustomerName")]
        [Required]
        public string CustomerName { get; set; }

        //gender
        [BindProperty]
        [Display(Name = "Gender")]
        [Required]
        public string Gender { get; set; }
        public string[] Genders = new[] { "M", "F", "U" };
        [BindProperty]
        //date
        [DataType(DataType.Date)]
        [Display(Name = "Dob")]
        [Required]
        public DateTime Dob { get; set; }

        //PhoneNumber
        [BindProperty]
        [Display(Name = "MobileNumber")]
        [Required]
        public string MobileNumber { get; set; }

        //Address
        [BindProperty]
        [Display(Name = "EmailId")]
        [Required]
        public string EmailId { get; set; }

        //City
        [BindProperty]
        [Display(Name = "City")]
        [Required]
        public string City { get; set; }

        //State
        [BindProperty]
        [Display(Name = "State")]
        [Required]
        public string State { get; set; }

        

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        private List<SelectListItem> GetCustomers()
        {
            var customerDataAccess = new CustomerDataAccess();
            var customerList = customerDataAccess.GetAll();

            var customerSelectList = new List<SelectListItem>();
            foreach (var customer in customerList)
            {
                customerSelectList.Add(new SelectListItem
                {
                    Text = $"{customer.CustomerName}-{customer.MobileNumber}",
                    Value = customer.CustomerId.ToString(),
                });
            }
            return customerSelectList;
        }

        public AddModel()
        {
            Dob = DateTime.Now.AddYears(-5);
        }
        public void OnGet()
        {
            CustomerName = "";
            Gender = "";
            Dob = DateTime.Now.AddYears(-5);
            MobileNumber = "";
            EmailId = "";
            City = "";
            State = "";
           


        }
        public void OnPost()
        {

            if (!ModelState.IsValid)
            {
                ErrorMessage = "Error!! Add failed.Please try Again";
                return;
            }
            var customerDataAccess = new CustomerDataAccess();
            var newcustomer = new CustomerDataModel
            {
               CustomerName = CustomerName,
                Gender = Gender,
                Dob= DateTime.Now,
                MobileNumber = MobileNumber,
                EmailId= EmailId,
                City = City,
                State = State,
               

            };
            var insertedCustomer = customerDataAccess.Insert(newcustomer);
            if (insertedCustomer != null)
            {
                SuccessMessage = $"successfully Inserted Customer{insertedCustomer.CustomerId}";
                ModelState.Clear();
            }
            else
            {
                ErrorMessage = $"Error!! Add failed.Please try Again - {customerDataAccess.ErrorMessage}";
            }


        }
    }
}
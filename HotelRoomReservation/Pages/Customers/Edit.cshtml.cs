using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HotelRoomReservation.DataAccess;
using HotelRoomReservation.Models;
using System.ComponentModel.DataAnnotations;

namespace HotelRoomReservation.Pages.Customers
{
    public class EditModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int CustomerId { get; set; }
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


       
        //MobileNumber
        [BindProperty]
        [Display(Name = "MobileNumber")]
        [Required]
        public string MobileNumber { get; set; }
        //EmailId
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
                    Text = $"{customer.CustomerName}-{customer.CustomerId}",
                    Value = customer.CustomerId.ToString(),
                });
            }
            return customerSelectList;
        }
       
        
        public void OnGet(int CustomerId)
        {
            CustomerId = CustomerId;
            if (CustomerId <= 0)
            {
                ErrorMessage = "Invalid CustomerId";
                return;
            }
            var customerDataAccess = new DataAccess.CustomerDataAccess();
            var c = customerDataAccess.GetCustomerById(CustomerId);
            if (c != null)
            {
                CustomerName = c.CustomerName;
                Gender = c.Gender;
                Dob = c.Dob;
                EmailId = c.EmailId;
                MobileNumber = c.MobileNumber;
                EmailId = c.EmailId;
                City = c.City;
                State = c.State;
               
               
            }
            else
            {

                ErrorMessage = "No Records found with CustomerId";
            }

        }
        public void OnPost()
        {
            
           
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Error!! Update failed.Please try Again";
                return;
            }
            //update
            var customerDataAccess = new DataAccess.CustomerDataAccess();
            var cusToUpdate = new CustomerDataModel
            {
                CustomerId = CustomerId,
                CustomerName = CustomerName,
                Gender = Gender,
                Dob = Dob,
                MobileNumber = MobileNumber,
                EmailId = EmailId,
                City = City,
                State = State,

               
               
            };
            var updatedCustomer = customerDataAccess.Update(cusToUpdate);
            if (updatedCustomer != null)
            {
                SuccessMessage = $"Customer {updatedCustomer.CustomerId} updated Successfully";
                Response.Headers.Add("Refresh", "3;URL=/Customers/List");
            }
            else
            {
                ErrorMessage = $"Error..!!!  Updating Customer Details. -'{customerDataAccess.ErrorMessage}'";
            }

        }
    }
}

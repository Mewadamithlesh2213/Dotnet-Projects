using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
      
        public  string Address { get; set; }
      

        public string  city { get; set; }
     

        public string State { get; set; }
       

        public string  Pincode { get; set; }

    }
}
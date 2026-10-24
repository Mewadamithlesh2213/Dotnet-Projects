using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MyApp.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Count { get; set; }

        public string ApplicationUserId { get; set; }
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
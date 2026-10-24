using Microsoft.AspNetCore.Mvc;
using MyApp.DataAccessLayer.Infrastructure.IRepository;
using MyApp.Models.ViewModels;
using System.Security.Claims;

namespace MywebApplication.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        // Property ka naam 'CartVM' (Capital VM)
        public CartVM CartVM { get; set; }

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);

            if (claims == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            var cartItems = _unitOfWork.Cart.GetAll(x => x.ApplicationUserId == claims.Value, "Product");

            CartVM = new CartVM()
            {
                ListOfCart = cartItems,
                Total = 0
            };

            if (CartVM.ListOfCart != null)
            {
                foreach (var item in CartVM.ListOfCart)
                {
                    // Agar Product null aa raha hoga toh exception se bachane ke liye null-check
                    if (item.Product != null)
                    {
                        CartVM.Total += (item.Product.Price * item.Count);
                    }
                }
            }

            return View(CartVM);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyApp.DataAccessLayer.Infrastructure.IRepository;
using MyApp.Models;
using MyApp.Models.ViewModels;
using System.IO;
using System.Linq;

namespace MywebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnviroment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnviroment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnviroment = hostingEnviroment;
        }

        #region APICALL 
        [HttpGet]
        public IActionResult AllProduct()
        {
            var products = _unitOfWork.Product.GetAll(includeProperties: "Category");
            return Json(new { data = products });
        }
        #endregion 

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateUpdate(int? id)
        {
            ProductVM vm = new ProductVM()
            {
                Product = new Product(),
                categories = _unitOfWork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };

            // CREATE MODE
            if (id == null || id == 0)
            {
                return View(vm);
            }
            // UPDATE MODE
            else
            {
                vm.Product = _unitOfWork.Product.GetT(x => x.Id == id);
                if (vm.Product == null)
                {
                    return NotFound();
                }

                return View(vm);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUpdate(ProductVM vm, IFormFile? file)
        {
            // categories validation bypass karna zaroori hai kyunki dropdown values alag bind hoti hain
            ModelState.Remove("categories");

            if (ModelState.IsValid)
            {
                string uploadDir = Path.Combine(_hostingEnviroment.WebRootPath, "ProductImg");

                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + "-" + Path.GetFileName(file.FileName);
                    string filePath = Path.Combine(uploadDir, fileName);

                    // Agar purani image exist karti hai toh usko delete karein
                    if (!string.IsNullOrEmpty(vm.Product.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(_hostingEnviroment.WebRootPath, vm.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    // Web relative path save karein
                    vm.Product.ImageUrl = @"\ProductImg\" + fileName;
                }

                if (vm.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(vm.Product);
                    TempData["msg"] = "Product created successfully!";
                }
                else
                {
                    _unitOfWork.Product.Update(vm.Product);
                    TempData["msg"] = "Product updated successfully!";
                }

                _unitOfWork.save();
                return RedirectToAction("Index");
            }

            // Agar validation fail ho, toh categories list repopulate karein taaki View par dropdown render ho sake
            vm.categories = _unitOfWork.Category.GetAll().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            return View(vm);
        }

        #region DeleteAPICALL
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var product = _unitOfWork.Product.GetT(x => x.Id == id);

            if (product == null)
            {
                return Json(new { success = false, message = "Error while deleting: Product not found!" });
            }

            // Purani image delete karein agar path exist karta ho
            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                var oldImagePath = Path.Combine(_hostingEnviroment.WebRootPath, product.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            _unitOfWork.Product.Delete(product);
            _unitOfWork.save();

            return Json(new { success = true, message = "Product deleted successfully!" });
        }
        #endregion
    }
}
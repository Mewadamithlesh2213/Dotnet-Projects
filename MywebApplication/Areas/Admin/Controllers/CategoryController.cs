using Microsoft.AspNetCore.Mvc;
using MyApp.DataAccessLayer.Infrastructure.IRepository;
using MyApp.Models;
using MyApp.Models.ViewModels;
using MywebApplication.Data;



namespace MywebApplication.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class CategoryController : Controller

    {

        private IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()

        {
            CateegoryVM cateegoryVM = new CateegoryVM();
            cateegoryVM.categories = _unitOfWork.Category.GetAll();

            return View(cateegoryVM);
        }

        //[HttpGet]
        //public IActionResult Create()

        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Create(Category ct)

        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Category.Add(ct);
        //        _unitOfWork.save();
        //        TempData["msg"] = "Data Add SuccessFully";
        //        return RedirectToAction("Index");
        //    }
        //    return View(ct);

        //}


        //[HttpGet]
        //public IActionResult CreateUpdate(int? id)

        //{
        //    CateegoryVM vm  = new CateegoryVM();
        //    if (id == null || id == 0)
        //    {
        //        return View(vm);
        //    }
        //    else
        //    {
        //        var Editcategory = _unitOfWork.Category.GetT(x => x.Id == id);
        //        if (Editcategory == null)
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            return View(Editcategory);
        //        }
        //    }

        //}



        //[HttpPost]

        //public IActionResult CreateUpdate(CateegoryVM vm)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Category.Add(vm.Category);
        //        _unitOfWork.save(); 
        //        TempData["msg"] = "Data Edit SuccessFully";
        //        return RedirectToAction("Index");
        //    }
        //    return RedirectToAction("Index");
        //}



        [HttpGet]
        public IActionResult CreateUpdate(int? id)
        {
            CateegoryVM vm = new CateegoryVM();

            // Zaroori: Pehle Category object ko initialize karo taaki view mein NullReferenceException na aaye
            vm.Category = new Category();

            if (id == null || id == 0)
            {
                // CREATE MODE: Naya form khulega, khaali VM return hoga
                return View(vm);
            }
            else
            {
                // UPDATE MODE: Database se purana data nikalenge
               vm.Category = _unitOfWork.Category.GetT(x => x.Id == id);
                if (vm.Category == null)
                {
                    return NotFound();
                }

                // BUG FIX: Pura object direct View mein nahi daalna hai!
                // Editcategory ko VM ki Category property mein set karke 'vm' return karna hai.
                
                return View(vm);
            }
        }

        [HttpPost]
        public IActionResult CreateUpdate(CateegoryVM vm)
        {
            // BUG FIX: categories list form se submit nahi hoti, isliye uski validation check remove karni padegi
            ModelState.Remove("categories");

            if (ModelState.IsValid)
            {
                // BUG FIX: Agar Id == 0 hai toh Add hoga, nahi toh Update hoga
                if (vm.Category.Id == 0)
                {
                    _unitOfWork.Category.Add(vm.Category);
                    TempData["msg"] = "Data Created SuccessFully";
                }
                else
                {
                    _unitOfWork.Category.Update(vm.Category); // Aapke Repository mein Update method hona chahiye
                    TempData["msg"] = "Data Update SuccessFully";
                }

                _unitOfWork.save();
                return RedirectToAction("Index");
            }

            // Agar validation fail ho jaye (jaise blank field chhod di), toh wapas usi view par data ke sath bhejein
            return View(vm);
        }


        [HttpGet]
        public IActionResult Delete(int? id)

        {
            if (id == null)
            {
                return NotFound();
            }
            var ct = _unitOfWork.Category.GetT(x => x.Id == id);
            if (ct != null)
            {
                return View(ct);
            }
            else
            {
                return NotFound();
            }

            return View();
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteData(int? id)
        {
            var ct = _unitOfWork.Category.GetT(x => x.Id == id);

            if (ct == null){
                return NotFound();
            }
            _unitOfWork.Category.Delete(ct);
            _unitOfWork.save();
            TempData["msg"] = "Data Delete SuccessFully";
            return RedirectToAction("Index");
        }
    }
}

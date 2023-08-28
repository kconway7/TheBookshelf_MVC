using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TheBookshelf.DataAccess.Repository.IRepository;
using TheBookshelf.Models;
using TheBookshelf.Models.ViewModels;

namespace TheBookshelfWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();

            return View(objProductList);
        }

        public IActionResult Upsert(int? id)
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem(u.Name, u.Id.ToString()));

            //ViewBag.categoryList = CategoryList;            
            //ViewData["CategoryList"] = CategoryList;
            ProductVM productVM = new()
            {
                Product = new Product(),
                CategoryList = CategoryList
            };

            if (id != null || id == 0)
            {
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
            }

            return View(productVM);
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem(u.Name, u.Id.ToString()));

                return View(productVM);
            }
        }

        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }

        //    Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
        //    //Product? ProductFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
        //    //Product? ProductFromDb2 = _db.Categories.Where(u=>u.Id == id).FirstOrDefault();

        //    if (productFromDb == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(productFromDb);
        //}

        //[HttpPost]
        //public IActionResult Edit(Product obj)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Product.Update(obj);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Product edited successfully";
        //        return RedirectToAction("Index");
        //    }

        //    return View();

        //}

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);

            if (productFromDb == null)
            {
                return NotFound();
            }

            return View(productFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product? obj = _unitOfWork.Product.Get(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");

        }
    }
}

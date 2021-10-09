using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using rocky.Data;
using rocky.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using rocky.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace rocky.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _WebHostEnvironment;
        public ProductController(ApplicationDbContext db, IWebHostEnvironment WebHostEnvironment)
        {
            _db = db;
            _WebHostEnvironment = WebHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> objList = _db.Product;
            return View(objList);
        }

        [HttpGet]
        public IActionResult UpSert(int? Id) {

            //IEnumerable<SelectListItem> CategoryDropDown = _db.Category.Select(i => new SelectListItem
            //{
            //    Text = i.Name,
            //    Value = i.Id.ToString()
            //});
            //ViewBag.CategoryDropDown = CategoryDropDown;

            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategorySelectList = _db.Category.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            if (Id == null)
            {
                // This for Create
                return View(productVM);
            }
            else
            {
                // This for Update.
                productVM.Product = _db.Product.Find(Id);
                if (productVM.Product == null) {
                    return NotFound();
                }
                return View(productVM);
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult UpSert(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _WebHostEnvironment.WebRootPath;
                if (productVM.Product.Id == 0)
                {
                    // create
                    string upload = webRootPath + WebConstants.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extention = Path.GetExtension(files[0].FileName);
                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extention), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    productVM.Product.Image = fileName + extention;
                    _db.Product.Add(productVM.Product);
                    _db.SaveChanges();
                }
                else
                {
                    // update
                }
            }
            return View(productVM);
        }



    }
}

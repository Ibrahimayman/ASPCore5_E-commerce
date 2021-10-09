using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using rocky.Data;
using rocky.Models;

namespace rocky.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> CategoryList = _db.Category;
            return View(CategoryList);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Category.Add(category);
                _db.SaveChanges();
                return RedirectToAction("Index"); // Redirct to the same controller.
            }
            return View(category);
        }


        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0) return NotFound();
            var Category = _db.Category.Find(Id);
            if (Category == null) return NotFound();
            return View(Category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Category.Update(category);
                _db.SaveChanges();
                return RedirectToAction("Index"); // Redirct to the same controller.
            }
            return View(category);
        }


        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0) return NotFound();
            var Category = _db.Category.Find(Id);
            if (Category == null) return NotFound();
            return View(Category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteRecord(int? Id)
        {
            Category category = _db.Category.Find(Id);
            if (category == null) return NotFound();
            _db.Category.Remove(category);
            _db.SaveChanges();
            return RedirectToAction("Index"); // Redirct to the same controller.
        }


    }
}

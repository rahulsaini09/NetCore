using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MvcApp.Data;
using MvcApp.Models;

namespace MvcApp.Controllers
{
    public class CategoryController : Controller
    {
        public readonly ApplicationDBContext _db;
        public CategoryController(ApplicationDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> categories = _db.Categories.ToList();
            return View(categories);
        }

        #region Add
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category added successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        #endregion

        #region Edit
        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0) { return NotFound(); }
            Category? category = _db.Categories.FirstOrDefault(x => x.Id == Id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        #endregion

        #region Delete
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0) { return NotFound(); }
            Category? category = _db.Categories.FirstOrDefault(x => x.Id == Id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? Id)
        {
            Category? category = _db.Categories.FirstOrDefault(x => x.Id == Id);
            if (category == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(category);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
        #endregion        
    }
}

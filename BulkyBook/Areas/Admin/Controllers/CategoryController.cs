using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            Category category = new Category();
            if(id == null)
            {
                // Creating a new category 
                return View(category);
            }

            // this if for edit 
            category = _unitOfWork.Category.Get(id.GetValueOrDefault());
            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category category)
        {
            // Check all of the validations in the model and checks if they are valid 
            if (ModelState.IsValid)
            {
                if (category.Id == 0)
                    _unitOfWork.Category.Add(category);
                else
                    _unitOfWork.Category.Update(category);

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        #region API Calls

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Category.GetAll();
            return Json(new { data = allObj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            // Get obj from the database
            var objFromDb = _unitOfWork.Category.Get(id);

            // Validate the object and return json if it is not present 
            if (objFromDb == null)
                return Json(new { success = false, message = "Error while deleting" });

            // Else remove the object and save the db, returning a success message 
            _unitOfWork.Category.Remove(objFromDb);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Successfully deleted" });
        }


        #endregion

    }
}

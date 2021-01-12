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
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            var company = new Company();

            // This will determine if we are creating new company or editing old one 
            if (id == null)
                return View(company);

            // We are editing old company -> populate the values from db 
            company = _unitOfWork.Company.Get(id.GetValueOrDefault());

            // Check if there was an error and return not found if there is 
            if (company == null)
                return NotFound();

            // Return company object to the upsert page 
            return View(company);
        }

        #region Api Calls

        // Gets all of the companies from the db and return as data in json format for when the api gets
        // called by the json script 
        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Company.GetAll();
            return Json(new { data = allObj });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company company)
        {
            if (ModelState.IsValid)
            {
                if (company.Id == 0)
                    _unitOfWork.Company.Add(company);
                else
                    _unitOfWork.Company.Update(company);

                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(company);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            // Get the object from the database 
            var objFromDb = _unitOfWork.Company.Get(id);

            // Validate that the oject is not null 
            if (objFromDb == null)
                return Json(new { success = false, message = "Error while deleting" });

            _unitOfWork.Company.Remove(objFromDb);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Successfully deleted" });
        }


        #endregion
    }
}

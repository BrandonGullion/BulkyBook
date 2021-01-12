using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using BulkyBook.Utility;
using Dapper;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        #region Properties
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor 

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.SP_Call.List<CoverType>(StaticDetails.Proc_CoverType_GetAll);
            return Json(new { data = allObj });
        }

        public IActionResult Upsert(int? id)
        {
            CoverType coverType = new CoverType();
            if (id == null)
                return View(coverType);

            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            coverType = _unitOfWork.SP_Call.OneRecord<CoverType>(StaticDetails.Proc_CoverType_Get, parameter);

            if (coverType == null)
                return NotFound();

            return View(coverType);
        }

        // Used when submitting the new or updated coverType model via button press 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CoverType coverType)
        {
            // Save if the model is valid 
            if (ModelState.IsValid)
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Name", coverType.Name);
                if (coverType.Id == 0)
                    _unitOfWork.SP_Call.Execute(StaticDetails.Proc_CoverType_Create, parameter);
                else
                {
                    parameter.Add("@Id", coverType.Id);
                    _unitOfWork.SP_Call.Execute(StaticDetails.Proc_CoverType_Update, parameter);
                }

                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(coverType);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            // Get CoverType from the DB
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            var ObjFromDb = _unitOfWork.SP_Call.OneRecord<CoverType>(StaticDetails.Proc_CoverType_Get, parameter);

            // Return Json false success if there is an error saving 
            if (ObjFromDb == null)
                return Json(new { success = false, message = "Error while deleting" });

            // Else Remove the object and save the db, returning a success message 
            _unitOfWork.SP_Call.Execute(StaticDetails.Proc_CoverType_Delete, parameter);
            _unitOfWork.Save();

            return Json( new { success = true, message = "Successfully deleted"});
        }

        #endregion
    }
}

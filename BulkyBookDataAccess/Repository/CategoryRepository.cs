using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.DataAcess.Data;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BulkyBook.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        #region Properties 

        private readonly ApplicationDbContext _context;

        #endregion

        #region Constructor 

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        // Takes in a newly updated category class and checks for it in the db, it then updates the corresponding values
        // and saves the changes if the object is not null 
        public void Update(Category category)
        {
            var objFromDb = _context.Catergories.FirstOrDefault(c => c.Id == category.Id);
            if(objFromDb != null)
            {
                objFromDb.Name = category.Name;
            }
        }

        #endregion
    }
}

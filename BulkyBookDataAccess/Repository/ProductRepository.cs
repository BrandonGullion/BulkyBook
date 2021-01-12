using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.DataAcess.Data;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        #region Properties 

        private readonly ApplicationDbContext _context;

        #endregion

        #region Constructor 

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        // Takes in a newly updated category class and checks for it in the db, it then updates the corresponding values
        // and saves the changes if the object is not null 
        public void Update(Product product)
        {
            var objFromDb = _context.Products.FirstOrDefault(c => c.Id == product.Id);
            if(objFromDb != null)
            {
                if (product.ImageUrl != null)
                    objFromDb.ImageUrl = product.ImageUrl;

                objFromDb.Title = product.Title;
                objFromDb.ISBN = product.ISBN;
                objFromDb.Price = product.Price;
                objFromDb.ListPrice = product.ListPrice;
                objFromDb.Price100 = product.Price100;
                objFromDb.Description = product.Description;
                objFromDb.CategoryId = product.CategoryId;
                objFromDb.Author = product.Author;
                objFromDb.CoverTypeId = product.CoverTypeId;
            }
        }

        #endregion
    }
}

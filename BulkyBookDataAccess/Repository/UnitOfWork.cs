using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.DataAcess.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Properties 

        private readonly ApplicationDbContext _context;
        public ICategoryRepository Category { get; private set; }
        public ICoverTypeRepository CoverType { get; private set; }
        public IProductRepository Product { get; private set; }
        public ICompanyRepository Company { get; set; }

        public ISP_Call SP_Call { get; private set; }

        #endregion

        #region Constructor 

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
            Product = new ProductRepository(_context);
            CoverType = new CoverTypeRepository(_context);
            Company = new CompanyRepository(_context);
            SP_Call = new SP_Call(_context);
        }

        #endregion

        #region Methods


        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        #endregion
    }
}

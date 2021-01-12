using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.DataAcess.Data;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkyBook.DataAccess.Repository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        #region Properties 

        private readonly ApplicationDbContext _context;

        #endregion

        #region Constructor 

        public CoverTypeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        #endregion

        public void Update(CoverType coverType)
        {
            var objFromDb = _context.Catergories.FirstOrDefault(c => c.Id == coverType.Id);
            if(objFromDb != null)
            {
                objFromDb.Name = coverType.Name;
            }
        }
    }
}

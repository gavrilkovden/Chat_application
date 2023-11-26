using DataAccessLayer.EntityDB;
using DataAccessLayer.Repository.generic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.generic
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ChatDbContext _context;

        public UnitOfWork(ChatDbContext context)
        {
            _context = context;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

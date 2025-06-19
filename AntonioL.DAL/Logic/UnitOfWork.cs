using AntonioL.DAL.Interfaces;
using AntonioL.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntonioL.DAL.Logic
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OL_Gamers_ALEntities _context;
        private IVideojuegoRepository _videojuegos;
        private ICompaniaRepository _companias;
        public UnitOfWork()
        {
            _context = new OL_Gamers_ALEntities();
        }

        public IVideojuegoRepository Videojuegos {
            get
            {
                if (_videojuegos == null)
                    _videojuegos = new VideojuegoRepository(_context);
                return _videojuegos;
            }
        }
        public ICompaniaRepository Companias {
            get
            {
                if (_companias == null)
                    _companias = new CompaniaRepository(_context);
                return _companias;
            }
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

using AntonioL.DAL.Interfaces;
using AntonioL.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntonioL.DAL.Logic
{
    public class CompaniaRepository : Repository<Compania>, ICompaniaRepository
    {
        public CompaniaRepository(OL_Gamers_ALEntities context) : base(context) { }

        public IEnumerable<Compania> GetByName(string name)
        {
            return Context.Companias
                .Where(c => c.nombre.Contains(name))
                .ToList();
        }

        public IEnumerable<Compania> GetSortedByName()
        {
            return Context.Companias
                .OrderBy(c => c.nombre)
                .ToList();
        }

        public IEnumerable<Compania> GetWithVideojuegos()
        {
            return Context.Companias
                .Include("Videojuegos")  
                .ToList();
        }

        public Compania GetWithVideojuegos(int id)
        {
            return Context.Companias
                .Include("Videojuegos")  
                .FirstOrDefault(c => c.id_compania == id);
        }
    }
}

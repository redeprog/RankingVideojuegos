using AntonioL.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntonioL.DAL.Interfaces
{
    public interface ICompaniaRepository : IRepository<Compania>
    {
        // <summary>
        /// Obtiene compañías por nombre (búsqueda parcial)
        /// </summary>
        IEnumerable<Compania> GetByName(string name);

        /// <summary>
        /// Obtiene compañías ordenadas por nombre
        /// </summary>
        IEnumerable<Compania> GetSortedByName();

        /// <summary>
        /// Obtiene compañías con sus videojuegos relacionados (carga eager)
        /// </summary>
        IEnumerable<Compania> GetWithVideojuegos();

        /// <summary>
        /// Obtiene una compañía con sus videojuegos
        /// </summary>
        Compania GetWithVideojuegos(int id);
    }
}

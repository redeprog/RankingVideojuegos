using AntonioL.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntonioL.DAL.Interfaces
{
    public interface IVideojuegoRepository : IRepository<Videojuego>
    {
        // Métodos específicos de Videojuegos
        IEnumerable<Videojuego> GetByCompania(int companiaId);
        IEnumerable<Videojuego> GetTopRated(int count);
        IEnumerable<Videojuego> GetByYear(int year);
        IEnumerable<Videojuego> GetWithCompania();
    }
}

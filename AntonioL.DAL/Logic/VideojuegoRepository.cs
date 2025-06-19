using AntonioL.DAL.Interfaces;
using AntonioL.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntonioL.DAL.Logic
{
    public class VideojuegoRepository : Repository<Videojuego>, IVideojuegoRepository
    {
        public VideojuegoRepository(OL_Gamers_ALEntities context) : base(context) { }

        public IEnumerable<Videojuego> GetByCompania(int companiaId)
        {
            return Context.Videojuegos
                .Where(v => v.id_compania == companiaId)
                .ToList();
        }

        public IEnumerable<Videojuego> GetTopRated(int count)
        {
            return Context.Videojuegos
                .OrderByDescending(v => v.puntaje_promedio)
                .Take(count)
                .ToList();
        }

        public IEnumerable<Videojuego> GetByYear(int year)
        {
            return Context.Videojuegos
                .Where(v => v.anio_lanzamiento == year)
                .ToList();
        }

        public IEnumerable<Videojuego> GetWithCompania()
        {
            return Context.Videojuegos
                .Include("Compania")
                .ToList();
        }
    }
}

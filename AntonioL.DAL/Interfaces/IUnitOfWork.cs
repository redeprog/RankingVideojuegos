using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntonioL.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IVideojuegoRepository Videojuegos { get; }
        ICompaniaRepository Companias { get; }
        int Complete();
    }
}

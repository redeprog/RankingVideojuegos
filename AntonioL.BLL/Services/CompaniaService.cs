using AntonioL.BLL.Interfaces;
using AntonioL.DAL.Interfaces;
using AntonioL.Share.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntonioL.BLL.Services
{
    public class CompaniaService : ICompaniaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompaniaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<CompaniaDTO> GetAll()
        {
            return _unitOfWork.Companias.GetAll()
                .Select(c => new CompaniaDTO
                {
                    Id = c.id_compania,
                    Nombre = c.nombre,
                    CantidadVideojuegos = c.Videojuegos?.Count ?? 0
                });
        }

        public CompaniaDTO GetById(int id)
        {
            var compania = _unitOfWork.Companias.GetWithVideojuegos(id);
            return new CompaniaDTO
            {
                Id = compania.id_compania,
                Nombre = compania.nombre,
                CantidadVideojuegos = compania.Videojuegos?.Count ?? 0
            };
        }
    }
}

using AntonioL.Share.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntonioL.BLL.Interfaces
{
    public interface IVideojuegoService
    {
        IEnumerable<VideojuegoDTO> GetAll();
        VideojuegoDTO GetById(int id);
        void Create(VideojuegoDTO videojuegoDto, string usuarioActualizacion);
        void Update(VideojuegoDTO videojuegoDto, string usuarioActualizacion);
        void Delete(int id);
        IEnumerable<CompaniaDTO> GetAllCompanias();
    }
}

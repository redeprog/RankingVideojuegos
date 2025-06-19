using AntonioL.Share.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntonioL.BLL.Interfaces
{
    public interface ICompaniaService
    {
        IEnumerable<CompaniaDTO> GetAll();
        CompaniaDTO GetById(int id);
    }
}

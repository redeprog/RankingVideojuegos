using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntonioL.Share.Dtos
{
    public class VideojuegoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? CompaniaId { get; set; }
        public string CompaniaNombre { get; set; }
        public int? AnioLanzamiento { get; set; }
        public decimal? Precio { get; set; }
        public decimal? Puntaje { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public string UsuarioActualizacionNombre { get; set; }
    }
}

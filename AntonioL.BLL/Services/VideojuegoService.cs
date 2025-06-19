using AntonioL.BLL.Interfaces;
using AntonioL.DAL;
using AntonioL.DAL.Interfaces;
using AntonioL.DAL.Models;
using AntonioL.Share.Dtos;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntonioL.BLL.Services
{
    public class VideojuegoService : IVideojuegoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public VideojuegoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<VideojuegoDTO> GetAll()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            return _unitOfWork.Videojuegos.GetWithCompania()
                .Select(v => new VideojuegoDTO
                {
                    Id = v.id_videojuego,
                    Nombre = v.nombre,
                    CompaniaId = v.id_compania,
                    CompaniaNombre = v.Compania?.nombre,
                    AnioLanzamiento = v.anio_lanzamiento,
                    Precio = v.precio,
                    Puntaje = v.puntaje_promedio,
                    FechaActualizacion = v.fecha_actualizacion,
                    UsuarioActualizacionNombre = userManager.FindById(v.usuario_actualizacion).UserName //v.usuario_actualizacion
                });
        }

        public VideojuegoDTO GetById(int id)
        {
            // Obtén el UserManager
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var videojuego = _unitOfWork.Videojuegos.Get(id);
            // Busca el usuario por ID
            var user = userManager.FindById(videojuego.usuario_actualizacion);
            return new VideojuegoDTO
            {
                Id = videojuego.id_videojuego,
                Nombre = videojuego.nombre,
                CompaniaId = videojuego.id_compania,
                AnioLanzamiento = videojuego.anio_lanzamiento,
                Precio = videojuego.precio,
                Puntaje = videojuego.puntaje_promedio,
                FechaActualizacion = videojuego.fecha_actualizacion,
                UsuarioActualizacionNombre = user.UserName
            };
        }

        public void Create(VideojuegoDTO videojuegoDto, string usuarioActualizacion)
        {
            var videojuego = new Videojuego
            {
                nombre = videojuegoDto.Nombre,
                id_compania = videojuegoDto.CompaniaId,
                anio_lanzamiento = videojuegoDto.AnioLanzamiento,
                precio = videojuegoDto.Precio,
                puntaje_promedio = videojuegoDto.Puntaje,
                fecha_actualizacion = DateTime.Now,
                usuario_actualizacion = usuarioActualizacion //HttpContext.Current.User.Identity.Name
            };

            _unitOfWork.Videojuegos.Add(videojuego);
            _unitOfWork.Complete();
        }

        public void Update(VideojuegoDTO videojuegoDto, string usuarioActualizacion)
        {
            var videojuego = _unitOfWork.Videojuegos.Get(videojuegoDto.Id);
            videojuego.nombre = videojuegoDto.Nombre;
            videojuego.id_compania = videojuegoDto.CompaniaId;
            videojuego.anio_lanzamiento = videojuegoDto.AnioLanzamiento;
            videojuego.precio = videojuegoDto.Precio;
            videojuego.puntaje_promedio = videojuegoDto.Puntaje;
            videojuego.fecha_actualizacion = DateTime.Now;
            videojuego.usuario_actualizacion = usuarioActualizacion; // HttpContext.Current.User.Identity.Name;

            _unitOfWork.Videojuegos.Update(videojuego);
            _unitOfWork.Complete();
        }

        public void Delete(int id)
        {
            var videojuego = _unitOfWork.Videojuegos.Get(id);
            _unitOfWork.Videojuegos.Remove(videojuego);
            _unitOfWork.Complete();
        }

        public IEnumerable<CompaniaDTO> GetAllCompanias()
        {
            return _unitOfWork.Companias.GetAll()
                .Select(c => new CompaniaDTO
                {
                    Id = c.id_compania,
                    Nombre = c.nombre,
                    CantidadVideojuegos = c.Videojuegos?.Count ?? 0
                });
        }
    }
}

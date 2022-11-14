using AutoMapper;
using WebApiPeliculas.DTOs;
using WebApiPeliculas.Entidades; 

namespace WebApiPeliculas.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<PeliculaDTO, Pelicula>();
            CreateMap<Pelicula, GetPeliculaDTO>();
            CreateMap<Pelicula, PeliculaDTOConDescripcion>()
                .ForMember(peliculaDTO => peliculaDTO.Descripcions, opciones => opciones.MapFrom(MapPeliculaDTODescripcion));
            CreateMap<DescripcionCreacionDTO, Descripcion>()
                .ForMember(Descripcion => Descripcion.peliculaDescripcions, opciones => opciones.MapFrom(MapPeliculaDescripcion));
            CreateMap<Descripcion, DescripcionDTO>();
            CreateMap<Descripcion, DescripcionDTOConPeliculas>()
                .ForMember(claseDTO => claseDTO.Peliculas, opciones => opciones.MapFrom(MapDescripcionDTOPeliculas));
            CreateMap<DescripcionPatchDTO, Descripcion>().ReverseMap();
        }

        private List<DescripcionDTO> MapPeliculaDTODescripcion(Pelicula pelicula, GetPeliculaDTO getPeliculaDTO)
        {
            var result = new List<DescripcionDTO>();

            if (pelicula.peliculaDescripcions == null) { return result; }

            foreach (var peliculaDescripcion in pelicula.peliculaDescripcions)
            {
                result.Add(new DescripcionDTO()
                {
                    Id = peliculaDescripcion.DescriptionId,
                    Nombre = peliculaDescripcion.Descripcion.Genero
                });
            }

            return result;
        }

        private List<GetPeliculaDTO> MapDescripcionDTOPeliculas(Descripcion clase, DescripcionDTO claseDTO)
        {
            var result = new List<GetPeliculaDTO>();

            if (clase.peliculaDescripcions == null)
            {
                return result;
            }

            foreach (var peliculaDescripcion in clase.peliculaDescripcions)
            {
                result.Add(new GetPeliculaDTO()
                {
                    Id = peliculaDescripcion.PeliculaId,
                    Nombre = peliculaDescripcion.Pelicula.Nombre
                });
            }

            return result;
        }

        private List<PeliculaDescripcion> MapPeliculaDescripcion(DescripcionCreacionDTO descripcionCreacionDTO, Descripcion descripcion)
        {
            var resultado = new List<PeliculaDescripcion>();

            if (descripcionCreacionDTO.PeliculasId == null) { return resultado; }
            foreach (var peliculaId in descripcionCreacionDTO.PeliculasId)
            {
                resultado.Add(new PeliculaDescripcion() {PeliculaId = peliculaId });
            }
            return resultado;
        }
    }
}

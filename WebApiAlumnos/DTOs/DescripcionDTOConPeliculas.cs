namespace WebApiPeliculas.DTOs
{
    public class DescripcionDTOConPeliculas: DescripcionDTO
    {
        public List<GetPeliculaDTO> Peliculas { get; set; }
    }
}

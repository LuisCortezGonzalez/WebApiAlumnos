namespace WebApiPeliculas.DTOs
{
    public class PeliculaDTOConDescripcion: GetPeliculaDTO
    {
        public List<DescripcionDTO> Descripcions { get; set; }
    }
}

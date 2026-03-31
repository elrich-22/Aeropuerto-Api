namespace AeropuertoAPI.Models
{
    public class RepuestoUtilizado
    {
        public int Id { get; set; }
        public int IdMantenimiento { get; set; }
        public int IdRepuesto { get; set; }
        public int Cantidad { get; set; }
    }
}
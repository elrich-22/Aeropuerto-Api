namespace AeropuertoAPI.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        public int IdVuelo { get; set; }
        public int IdPasajero { get; set; }
        public string NumeroAsiento { get; set; } = string.Empty;
        public string Clase { get; set; } = string.Empty;
        public DateTime FechaReserva { get; set; }
        public string Estado { get; set; } = string.Empty;
        public int EquipajeFacturado { get; set; }
        public decimal? PesoEquipaje { get; set; }
        public decimal TarifaPagada { get; set; }
        public string CodigoReserva { get; set; } = string.Empty;
    }
}
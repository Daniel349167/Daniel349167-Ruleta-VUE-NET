namespace RuletaAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Saldo { get; set; }
        public List<ApuestaTemporal> ApuestasTemporales { get; set; } = new List<ApuestaTemporal>(); // Apuestas temporales
    }
}

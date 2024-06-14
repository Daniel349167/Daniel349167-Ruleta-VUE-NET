namespace RuletaAPI.Responses
{
    public class ApuestasTemporalesResponse
    {
        public int UsuarioId { get; set; }
        public decimal SaldoTemporal { get; set; }
        public List<ApuestaResponse> Apuestas { get; set; } = new List<ApuestaResponse>(); // Inicializado para evitar warnings
    }
}

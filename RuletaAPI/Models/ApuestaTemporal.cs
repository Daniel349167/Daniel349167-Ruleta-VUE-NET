namespace RuletaAPI.Models
{
    public class ApuestaTemporal
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; } // Nullable para evitar warnings
        public string Tipo { get; set; } = string.Empty; // Inicializado para evitar warnings
        public string? Valor { get; set; } // Nullable para evitar warnings
        public decimal Monto { get; set; }
        public string? Color { get; set; } // Nullable para evitar warnings
        public string? Paridad { get; set; } // Nullable para evitar warnings
        public decimal Resultado { get; set; }
    }
}

namespace Proyecto2.Models
{
    public class Operacion
    {
        public int Id { get; set; }
        public string Expresion { get; set; }
        public double Resultado { get; set; }
        public int IdTipo { get; set; }
        public string NombreTipo { get; set; } 
    }
}

namespace Aliquota.Domain.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public DateTime DtAplicacao { get; set; }
        public DateTime DtResgate { get; set; }
        public double Valor { get; set; }
        public bool Ativo { get; set; }
        public int IdConta { get; set; }
    }
}

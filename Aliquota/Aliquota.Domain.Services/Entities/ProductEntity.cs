using Aliquota.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliquota.Domain.Entities
{
    public class ProductEntity : Entity
    {
        public DateTime DtAplicacao { get; set; }
        public DateTime DtResgate { get; set; }
        public double Valor { get; set; }
        public bool Ativo { get; set; }
        public int IdConta { get; set; }
        public virtual ContaEntity Conta { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Questao5.Domain.Entities
{
    public class Idempotencia : BaseEntity<Idempotencia>
    {
        public string Requisicao { get; set; }
        public string Resultado { get; set; }
    }
}

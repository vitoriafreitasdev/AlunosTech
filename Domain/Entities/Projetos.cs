using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Projetos
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public DateTime DataDeEntrega { get; set; }
    }
}

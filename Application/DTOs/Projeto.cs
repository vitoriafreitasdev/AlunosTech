using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public record Projeto
    {
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public DateTime DataDeEntrega { get; set; }
    }
}

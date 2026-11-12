using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public record Colaborador
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string Cargo { get; set; } = string.Empty;

        public decimal Salario { get; set; }

        public DateTime DataContratacao { get; set; }
    }
}

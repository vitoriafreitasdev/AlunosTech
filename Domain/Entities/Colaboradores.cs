using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Colaboradores
    {
        public int Id { get; init; }

        public string Nome { get; init; } = string.Empty;

        public string Email { get; init; } = string.Empty;

        public string Cargo { get; init; } = string.Empty;

        public decimal Salario { get; init; }

        public DateTime DataContratacao { get; init; }
    }
}

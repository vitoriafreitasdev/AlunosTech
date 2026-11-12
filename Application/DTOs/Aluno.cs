using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public record Aluno
    {
        public string Nome { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string AnoLetivo { get; set; } = string.Empty;

        public DesempenhoCategorias Desempenho { get; set; }

        public DateTime DataDeMatriculacao { get; set; }
    }
}

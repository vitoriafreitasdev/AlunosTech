using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Alunos
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string AnoLetivo { get; set; } = string.Empty;

        public DesempenhoCategorias Desempenho { get; set; }

        public DateTime DataDeMatriculacao { get; set; }
    }
}

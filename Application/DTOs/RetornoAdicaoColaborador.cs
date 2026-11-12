using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public record RetornoAdicaoColaborador
    {
        public bool adicionado { get; init; }
        public string? mensagem { get; init; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public record RetornoProjetos
    {
        public bool sucesso { get; init; }

        public string? mensagem { get; init; }

        public List<Projeto> projetos { get; init; } = new List<Projeto>();
    }
}

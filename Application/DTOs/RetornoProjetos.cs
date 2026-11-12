using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public record RetornoAlunos
    {
        public bool sucesso { get; init; }

        public string? mensagem { get; init; }

        public List<Aluno> alunos { get; init; } = new List<Aluno>();
    }
}

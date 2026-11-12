using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
namespace Application.DTOs
{
    public record RetornoColaboradores
    {
        public bool sucesso { get; init; }

        public string? mensagem { get; init; }

        public List<Colaborador> colaboradores { get; init; } = new List<Colaborador>();
    }
}

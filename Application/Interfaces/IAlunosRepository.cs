using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IAlunosRepository
    {
        public Task<Alunos?> AdicionarAluno(Alunos aluno);

        public Task<List<Aluno>> ChamarAlunos();
    }
}

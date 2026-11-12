using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class AlunosRepository : IAlunosRepository
    {
        private readonly AppDbContext _dbContext;

        public AlunosRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async     Task<Alunos?> AdicionarAluno(Alunos aluno)
        {
            var alunoAdd = await _dbContext.Alunos.AddAsync(aluno);

            await _dbContext.SaveChangesAsync();

            return alunoAdd.Entity;
        }

        public async Task<List<Aluno>> ChamarAlunos()
        {
            var alunos = await _dbContext.Alunos
              .AsNoTracking()
              .Select(
                  p => new Aluno
                  {
                      Nome = p.Nome,
                      Email = p.Email,
                      AnoLetivo = p.AnoLetivo,
                      Desempenho = p.Desempenho,
                      DataDeMatriculacao = p.DataDeMatriculacao,
                  }
              ).ToListAsync();

              return alunos;
        }
    }
}

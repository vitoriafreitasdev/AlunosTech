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
    public class ProjetosRepository : IProjetosRepository
    {
        private readonly AppDbContext _dbContext;
        public ProjetosRepository(AppDbContext context)
        {
            _dbContext = context;
        }
        public async Task<Projetos?> AdicionarProjeto(Projetos projeto)
        {
            var projetoAdd = await _dbContext.Projetos.AddAsync(projeto);

            await _dbContext.SaveChangesAsync();

            return projetoAdd.Entity;
        }

        public async Task<List<Projeto>> RetornarProjetos()
        {
            var projetos = await _dbContext.Projetos
                .AsNoTracking()
                .Select(
                    p => new Projeto
                    {
                        Nome = p.Nome,
                        Descricao = p.Descricao,
                        DataDeEntrega = p.DataDeEntrega
                    }
                ).ToListAsync();

            return projetos;
        }
    }
}


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
    public class ColaboradorRepository : IColaboradorRepository
    {
        private AppDbContext _dbContext;
        public ColaboradorRepository(AppDbContext context) 
        {
            _dbContext = context;
        }

        public async Task<Colaboradores?> AdicionarColaborador(Colaborador colaborador)
        {
            Colaboradores colaboradorEntity = new Colaboradores()
            { 
                Nome = colaborador.Nome,
                Email = colaborador.Email,
                Cargo = colaborador.Cargo,
                Salario = colaborador.Salario,
                DataContratacao = colaborador.DataContratacao
            };

            var adicao = await _dbContext.Colaboradores.AddAsync(colaboradorEntity);

            await _dbContext.SaveChangesAsync();

            if(adicao == null ) return null;

            return adicao.Entity;

        }
        //Função ChamarColaboradores dentro do ColaboradorRepository
        public async Task<List<Colaborador>> ChamarColaboradores()
        {
            var colaboradores = await _dbContext.Colaboradores
                .AsNoTracking()
                .Select(
                    p => new Colaborador()
                        {
                            Nome = p.Nome,
                            Email = p.Email,
                            Cargo = p.Cargo,
                            Salario = p.Salario,
                            DataContratacao = p.DataContratacao
                        }
                )
                .ToListAsync();

            return colaboradores;
        }
    }
}

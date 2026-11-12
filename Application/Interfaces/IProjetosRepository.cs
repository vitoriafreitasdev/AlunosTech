using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Application.DTOs;
namespace Application.Interfaces
{
    public interface IProjetosRepository
    {

        public Task<Projetos?> AdicionarProjeto(Projetos projeto);
        public Task<List<Projeto>> RetornarProjetos();

    }
}

using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Application.DTOs;

namespace Application.Interfaces
{
    public interface IColaboradorRepository
    {
        public Task<Colaboradores?> AdicionarColaborador(Colaborador colaborador);

        public Task<List<Colaborador>> ChamarColaboradores();
    }
}

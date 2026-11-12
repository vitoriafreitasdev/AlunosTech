using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases
{
    public class AlunosUseCases
    {
        private readonly IAlunosRepository _repository;

        public AlunosUseCases(IAlunosRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Adicionar(Aluno aluno)
        {
            try
            {
                var alunoEntidade = new Alunos()
                {
                    Nome = aluno.Nome,
                    Email = aluno.Email,
                    AnoLetivo = aluno.AnoLetivo,
                    Desempenho = aluno.Desempenho,
                    DataDeMatriculacao = aluno.DataDeMatriculacao
                };

                var adicionar = await _repository.AdicionarAluno(alunoEntidade);

                if (adicionar == null) return false;

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<RetornoAlunos> AlunosAdicionados()
        {
            try
            {
                var alunos = await _repository.ChamarAlunos();
                return new RetornoAlunos()
                {
                    sucesso = true,
                    mensagem = "Sucesso.",
                    alunos = alunos
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new RetornoAlunos()
                {
                    sucesso = false,
                    mensagem = "Erro no sistema, tente novamente."
                };

            }
        }
    }
}

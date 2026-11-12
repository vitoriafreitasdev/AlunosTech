
using Application.DTOs;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases
{
    public class ColaboradorUseCases
    {
        private IColaboradorRepository _repository;
        public ColaboradorUseCases(IColaboradorRepository repository)
        {
            _repository = repository;
        }

        public async Task<RetornoAdicaoColaborador> Adicionar(Colaborador colaborador)
        {
            try
            {
                var adicionado = await _repository.AdicionarColaborador(colaborador);
                if(adicionado == null)
                {
                    return new RetornoAdicaoColaborador()
                    {
                        adicionado = false,
                        mensagem = "Não foi possível adicionar, tente novamente."
                    };
                }
                return new RetornoAdicaoColaborador()
                {
                    adicionado = true,
                    mensagem = "Adicionado com sucesso."
                };

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new RetornoAdicaoColaborador()
                {
                    adicionado = false,
                    mensagem = "Não foi possível adicionar, tente novamente."
                };
            }

        }
        // função de RetornarColaboradores dentro de ColaboradorUseCases
        public async Task<RetornoColaboradores> RetornarColaboradores()
        {
            try
            {
                var colaboradores = await _repository.ChamarColaboradores();

                return new RetornoColaboradores()
                {
                    sucesso = true,
                    mensagem = "Busca realizada com sucesso.",
                    colaboradores = colaboradores
                };
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new RetornoColaboradores()
                {
                    sucesso = false,
                    mensagem = "Houve um erro no sistema, tente novamente mais tarde"
                };
            }
        }
    }
}

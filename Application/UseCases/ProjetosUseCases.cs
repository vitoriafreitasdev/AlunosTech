using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;


namespace Application.UseCases
{
    public class ProjetosUseCases
    {
        private readonly IProjetosRepository _projetosRepository;

        public ProjetosUseCases(IProjetosRepository repository) 
        {
            _projetosRepository = repository;
        }

        public async Task<bool> Adicionar(Projeto projeto)
        {
            try
            {
                var projetoEntidade = new Projetos()
                {
                    Nome = projeto.Nome,
                    Descricao = projeto.Descricao,
                    DataDeEntrega = projeto.DataDeEntrega
                };

                var adicionar = await _projetosRepository.AdicionarProjeto(projetoEntidade);

                if (adicionar == null) return false;

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        public async Task<RetornoProjetos> ProjetosAdicionados()
        {
            try
            {
                var projetos = await _projetosRepository.RetornarProjetos();
                return new RetornoProjetos()
                {
                    sucesso = true,
                    mensagem = "Sucesso.",
                    projetos = projetos
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new RetornoProjetos()
                { 
                    sucesso = false,
                    mensagem = "Erro no sistema, tente novamente."
                };

            }
        }
    }
}

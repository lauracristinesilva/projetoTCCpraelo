using infinitysky.Models;

namespace infinitysky.Repository
{
    public interface IPlanosRepositorio
    {
        List<Planos> ObterPlanosAleatorios(int IdPlano);

        List<Planos> ObterRestante(int IdPlano);


        IEnumerable<Planos> PesquisaPlanos(string Nome);

        public IEnumerable<Planos> ObterPlanosPorPaisId(int paisId);

        void Adicionar(Planos plano);

        // void Atualizar(Planos plano); // Se necessário
        Planos ObterPlano(long Id);

        void Apagar(long Id);
    }
}

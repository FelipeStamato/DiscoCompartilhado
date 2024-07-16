using DiscoCompartilhado.Handler;

namespace DiscoCompartilhado.Servicos
{
    public class DiscoAppServico
    {
        public static ArmazenamentoLogico PegaDisco(string consulta = null)
        {
            return new DiscoHandler(consulta).Executa();
        }
    }
}
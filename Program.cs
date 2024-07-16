using DiscoCompartilhadoBIOS.Servicos;

class Program
{
    static void Main()
    {
        ArmazenamentoLogico disco = DiscoAppServico.PegaDisco();
    }
}
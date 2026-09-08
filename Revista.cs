class Revista : MaterialBiblioteca
{
    public string NumeroEdicao { get; init; } = "";

    public Revista(int id, string titulo, int anopublicacao, string numeroedicao) : base(id, titulo, anopublicacao)
    {
        NumeroEdicao = numeroedicao;
    }

    public override int ObterPrazoEmDias()
    {
        return 3;
    }

    public override double ObterMultaPorDia()
    {
        return 1.00;
    }
}

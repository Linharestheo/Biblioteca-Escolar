class Livro : MaterialBiblioteca{
    public string Autor {get; init;} = "";

    public Livro (int id, string titulo, int anopublicacao, string autor) : base (id, titulo, anopublicacao)
    {
        Autor = autor;
    }

    public override int ObterPrazoEmDias()
    {
        return 7;
    }

    public override double ObterMultaPorDia()
    {
        return 1.50;
    }

}
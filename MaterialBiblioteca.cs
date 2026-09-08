abstract class MaterialBiblioteca : IEmprestavel
{
    public int Id { get; init; }
    public string Titulo { get; init; } = "";
    public int AnoPublicacao { get; init; }
    public bool Disponibilidade { get; private set; } = true;

    public MaterialBiblioteca(int id, string titulo, int anopublicacao)
    {
        if (id < 0)
        {
            throw new ArgumentException("Id inválido!");
        }
        if (string.IsNullOrWhiteSpace(titulo))
        {
            throw new ArgumentException("Titulo obrigatório!");
        }
        if (anopublicacao < 0)
        {
            throw new ArgumentException("Ano de publicação inválido!");
        }
        Id = id;
        Titulo = titulo;
        AnoPublicacao = anopublicacao;
    }

    public abstract int ObterPrazoEmDias();
    public abstract double ObterMultaPorDia();

    public bool Emprestar()
    {
        if (Disponibilidade == true)
        {
            Disponibilidade = false;
            return true;
        }
        return false;
    }

    public void Devolver()
    {
            Disponibilidade = true;
    }

}
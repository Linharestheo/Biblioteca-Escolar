class Emprestimo
{
    public Usuario Usuario { get; init; }
    public MaterialBiblioteca Material { get; init; }
    public DateTime DataEmprestimo { get; init; }
    public DateTime DataPrevistaDevolucao { get; init; }
    public DateTime? DataDevolucao { get; set; }

    public Emprestimo(Usuario usuario, MaterialBiblioteca material, DateTime dataemprestimo, DateTime dataprevistadevolucao)
    {
        Usuario = usuario;
        Material = material;
        DataEmprestimo = dataemprestimo;
        DataPrevistaDevolucao = dataprevistadevolucao;
    }

    public bool EstaAtrasado()
    {
        if (DataDevolucao == null && DateTime.Today > DataPrevistaDevolucao)
        {
            return true;
        }
        return false;
    }

    public double CalcularMulta()
    {
        DateTime datacomparacao = DataDevolucao ?? DateTime.Today;

        int diasatraso = (datacomparacao - DataPrevistaDevolucao).Days;

        if (diasatraso <= 0)
        {
            return 0;
        }
        return Material.ObterMultaPorDia() * diasatraso;
    }

    public void RegistrarDevolucao(DateTime datadevolucao)
    {
        if(DataDevolucao != null)
        {
            throw new ArgumentException("Devolução já registrada!");
        }
        DataDevolucao = datadevolucao;
        Material.Devolver();
    }
}
class Biblioteca
{
    private Dictionary<int, Usuario> usuarios = new Dictionary<int, Usuario>(); // colecoes usuarios
    private Dictionary<int, Livro> livros = new Dictionary<int, Livro>(); // colecoes livros
    private Dictionary<int, Revista> revistas = new Dictionary<int, Revista>(); // colecoes revistas
    private List<Emprestimo> emprestimos = new List<Emprestimo>(); // colecoes emprestimos

    public void CadastrarUsuario(int matricula, string nome, string email)
    {
        if (usuarios.ContainsKey(matricula))
        {
            throw new ArgumentException("Usuario já cadastrado!");
        }
        else
        {
            Usuario u = new Usuario(matricula, nome, email);
            usuarios.Add(matricula, u);
        }

    }

    public void CadastrarLivro(int id, string titulo, int anopublicacao, string autor)
    {
        if (livros.ContainsKey(id) || revistas.ContainsKey(id))
        {
            throw new ArgumentException("Código já cadastrado!");
        }
        Livro l = new Livro(id, titulo, anopublicacao, autor);
        livros.Add(id, l);
    }

    public void CadastrarRevista(int id, string titulo, int anopublicacao, string numeroedicao)
    {
        if (livros.ContainsKey(id) || revistas.ContainsKey(id))
        {
            throw new ArgumentException("Código já cadastrado!");
        }
        Revista r = new Revista(id, titulo, anopublicacao, numeroedicao);
        revistas.Add(id, r);
    }

    public void ListarUsuarios()
    {
        if (usuarios.Count == 0)
        {
            Console.WriteLine("Nenhum usuario cadastrado!");
            return;
        }
        foreach (var u in usuarios.Values)
        {
            Console.WriteLine($"Matricula: {u.Matricula} | Nome: {u.Nome} | Email: {u.Email}");
        }
    }

    public void ListarMateriais()
    {
        if (livros.Count == 0 && revistas.Count == 0)
        {
            Console.WriteLine("Nenhum material cadastrado!");
            return;
        }

        foreach (var l in livros.Values)
        {
            Console.WriteLine($"Codigo: {l.Id} | Titulo: {l.Titulo} | Ano: {l.AnoPublicacao} | Autor: {l.Autor} | Disponibilidade: {l.Disponibilidade} | Tipo: Livro");
        }

        foreach (var r in revistas.Values)
        {
            Console.WriteLine($"Codigo: {r.Id} | Titulo: {r.Titulo} | Ano: {r.AnoPublicacao} | Numero edicao: {r.NumeroEdicao} | Disponibilidade: {r.Disponibilidade} | Tipo: Revista");
        }
    }

    public void ConsultarMaterial(int id)
    {
        if (livros.ContainsKey(id))
        {
            string status = livros[id].Disponibilidade ? "Disponível" : "Indisponível";
            Console.WriteLine($"Tipo: Livro | Disponibilidade: {status}");
            return;
        }

        if (revistas.ContainsKey(id))
        {
            string status = revistas[id].Disponibilidade ? "Disponível" : "Indisponível";
            Console.WriteLine($"Tipo: Revista | Disponibilidade: {status}");
            return;
        }

        throw new ArgumentException("Material inexistente!");
    }

    public void RealizarEmprestimo(int matricula, int id)
    {
        if (!usuarios.ContainsKey(matricula)) // verificando se usuario existe
        {
            throw new ArgumentException("Usuario inexistente!");
        }

        if (!livros.ContainsKey(id) && !revistas.ContainsKey(id)) // verificando se material existe
        {
            throw new ArgumentException("Material inexistente!");
        }

        MaterialBiblioteca material; // verificando disponibilidade

        if (livros.ContainsKey(id))
        {
            material = livros[id];
        }
        else
        {
            material = revistas[id];
        }

        if (material.Disponibilidade == false)
        {
            throw new ArgumentException("Material indisponível!");
        }

        int qtdemprestimos = 0; // verificando quantos emprestimos
        foreach (var e in emprestimos)
        {
            if (e.Usuario.Matricula == matricula && e.DataDevolucao == null)
            {
                qtdemprestimos++;
            }
        }

        if (qtdemprestimos >= 3) // verificando quantidade de emprestimos
        {
            throw new ArgumentException("Limite de emprestimos atingido!");
        }

        // Realizando emprestimo
        Usuario u = usuarios[matricula];
        DateTime dataemprestimo = DateTime.Today;
        DateTime dataprevistadevolucao = DateTime.Today.AddDays(material.ObterPrazoEmDias());
        Emprestimo emp = new Emprestimo(u, material, dataemprestimo, dataprevistadevolucao);
        emprestimos.Add(emp);
        material.Emprestar();

    }

    public void RealizarDevolucao(int matricula, int id, DateTime? datadevolucao)
    {
        if (!usuarios.ContainsKey(matricula)) // verificando se usuario existe
        {
            throw new ArgumentException("Usuario inexistente!");
        }

        if (!livros.ContainsKey(id) && !revistas.ContainsKey(id)) // verificando material
        {
            throw new ArgumentException("Material inexistente!");
        }

        Emprestimo? empencontrado = null;

        foreach (var e in emprestimos)
        {
            if (e.Usuario.Matricula == matricula && e.Material.Id == id && e.DataDevolucao == null)
            {
                empencontrado = e;
                break;
            }
        }

        if (empencontrado == null)
        {
            throw new ArgumentException("Nenhum emprestimo ativo encontrado a este Usuario!");
        }

        empencontrado.RegistrarDevolucao(datadevolucao ?? DateTime.Today);

    }

    public void ExibirEmprestimosAtivos()
    {
        bool encontrouAtivo = false;

        foreach (var e in emprestimos)
        {
            if (e.DataDevolucao == null)
            {
                encontrouAtivo = true;
                string situacao = e.EstaAtrasado() ? "Atrasado" : "Em dia";
                Console.WriteLine($"Usuario: {e.Usuario.Nome} | Material: {e.Material.Titulo} | Data Emprestimo: {e.DataEmprestimo:dd/MM/yyyy} | Data prevista devolucao: {e.DataPrevistaDevolucao:dd/MM/yyyy} | Situação: {situacao}");
            }
        }
        if (!encontrouAtivo)
        {
            Console.WriteLine("Nenhum empréstimo ativo no momento.");
        }
    }

    public void GerarRelatorio()
    {
        int qtdusuarios = usuarios.Count;
        int qtdlivros = livros.Count;
        int qtdrevistas = revistas.Count;
        int materiaisdisponiveis = 0;
        int emprestimosativos = 0;
        int devolucoesatrasadas = 0;
        double totalmultas = 0;

        // contando materias disponiveis
        foreach (var l in livros.Values)
        {
            if (l.Disponibilidade == true)
            {
                materiaisdisponiveis++;
            }
        }
        foreach (var r in revistas.Values)
        {
            if (r.Disponibilidade == true)
            {
                materiaisdisponiveis++;
            }
        }

        foreach (var e in emprestimos)
        {
            if (e.DataDevolucao == null)
            {
                emprestimosativos++;
            }
            if (e.EstaAtrasado() == true)
            {
                devolucoesatrasadas++;
            }
            totalmultas += e.CalcularMulta();
        }
        Console.WriteLine("==================RELATORIO==================");
        Console.WriteLine($"Quantidade de Usuarios: {qtdusuarios}");
        Console.WriteLine($"Quantidade de Livros: {qtdlivros}");
        Console.WriteLine($"Quantidade de Revistas: {qtdrevistas}");
        Console.WriteLine($"Quantidade Materiais Disponíveis: {materiaisdisponiveis}");
        Console.WriteLine($"Quantidade Emprestimos Ativos: {emprestimosativos}");
        Console.WriteLine($"Quantidade Devolucoes Atrasadas: {devolucoesatrasadas}");
        Console.WriteLine($"Valor Total de Multas: R${totalmultas:F2}");
        Console.WriteLine("=============================================");
    }


}
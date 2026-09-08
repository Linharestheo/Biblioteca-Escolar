using System;
class Program
{
  static void Main()
  {
    Biblioteca biblioteca = new Biblioteca();
    bool continuar = true;
    int[] tiposValidos = [1, 2];

    do
    {
      Console.Clear();
      Console.WriteLine("================== BIBLIOTECA ESCOLAR ==================");
      Console.WriteLine("1 - Cadastrar usuário    | 6 - Realizar empréstimo");
      Console.WriteLine("2 - Cadastrar material   | 7 - Registrar devolução");
      Console.WriteLine("3 - Listar usuários      | 8 - Exibir empréstimos ativos");
      Console.WriteLine("4 - Listar materiais     | 9 - Exibir relatório");
      Console.WriteLine("5 - Consultar por código | 0 - Sair");
      Console.WriteLine("========================================================");
      Console.WriteLine();
      Console.Write("Escolha uma opcao: ");
      bool valido = int.TryParse(Console.ReadLine(), out int opcao);
      Console.WriteLine();
      if (!valido)
      {
        Console.WriteLine("Opção inválida! Digite um número.");
        continue;
      }

      switch (opcao)
      {
        case 0:
          {
            Console.Write("Tem certeza que deseja sair? (S/N): ");
            string confirmacao = Console.ReadLine() ?? "";
            if (confirmacao.ToUpper() == "S")
            {
              continuar = false;
            }
            break;
          }
        case 1:
          {
            Console.Write("Digite a matrícula: ");
            bool matriculavalida = int.TryParse(Console.ReadLine(), out int matricula);
            if (!matriculavalida)
            {
              Console.WriteLine("Digite uma matrícula válida!");
              break;
            }

            Console.Write("Digite o nome: ");
            string nome = Console.ReadLine() ?? "";

            Console.Write("Digite o e-mail: ");
            string email = Console.ReadLine() ?? "";

            try
            {
              biblioteca.CadastrarUsuario(matricula, nome, email);
              Console.WriteLine("Usuário cadastrado com sucesso!");
            }
            catch (ArgumentException erro)
            {

              Console.WriteLine("ERRO " + erro.Message);
            }
            break;
          }
        case 2:
          {
            Console.WriteLine("1 - Livro");
            Console.WriteLine("2 - Revista");
            Console.WriteLine("Escolha uma opcao: ");

            bool opcaovalida = int.TryParse(Console.ReadLine(), out int op);
            if (!opcaovalida || (!tiposValidos.Contains(op)))
            {
              Console.WriteLine("Opcao invalida!");
              break;
            }

            Console.Write("Digite o codigo: ");
            bool idvalido = int.TryParse(Console.ReadLine(), out int id);
            if (!idvalido)
            {
              Console.WriteLine("Digite um código válido!");
              break;
            }

            Console.Write("Digite o Titulo: ");
            string titulo = Console.ReadLine() ?? "";

            Console.Write("Digite o Ano de Publicacao: ");
            bool anovalido = int.TryParse(Console.ReadLine(), out int anopublicacao);
            if (!anovalido)
            {
              Console.WriteLine("Digite um Ano de Publicacao válido!");
              break;
            }

            if (op == 1)
            {
              Console.Write("Digite o Nome do Autor: ");
              string autor = Console.ReadLine() ?? "";
              try
              {
                biblioteca.CadastrarLivro(id, titulo, anopublicacao, autor);
                Console.WriteLine("Livro Cadastrado com Sucesso!");
              }
              catch (ArgumentException erro)
              {
                Console.WriteLine("Erro: " + erro.Message);
              }
            }
            else
            {
              Console.Write("Digite o Numero da Edicao: ");
              string numeroedicao = Console.ReadLine() ?? "";
              try
              {
                biblioteca.CadastrarRevista(id, titulo, anopublicacao, numeroedicao);
                Console.WriteLine("Revista Cadastrada com Sucesso!");
              }
              catch (ArgumentException erro)
              {

                Console.WriteLine("Erro: " + erro.Message);
              }
            }
            break;
          }
        case 3:
          {
            biblioteca.ListarUsuarios();
            break;
          }
        case 4:
          {
            biblioteca.ListarMateriais();
            break;
          }
        case 5:
          {
            Console.Write("Digite o codigo: ");
            bool idvalido = int.TryParse(Console.ReadLine(), out int id);
            if (!idvalido)
            {
              Console.WriteLine("Digite um codigo válido!");
              break;
            }

            try
            {
              biblioteca.ConsultarMaterial(id);
            }
            catch (ArgumentException erro)
            {

              Console.WriteLine("Erro: " + erro.Message);
            }
            break;
          }
        case 6:
          {
            Console.Write("Digite a matrícula: ");
            bool matriculavalida = int.TryParse(Console.ReadLine(), out int matricula);
            if (!matriculavalida)
            {
              Console.WriteLine("Digite uma matrícula válida!");
              break;
            }

            Console.Write("Digite o codigo: ");
            bool idvalido = int.TryParse(Console.ReadLine(), out int id);
            if (!idvalido)
            {
              Console.WriteLine("Digite um código válido!");
              break;
            }

            try
            {
              biblioteca.RealizarEmprestimo(matricula, id);
              Console.WriteLine("Empréstimo realizado com sucesso!");
            }
            catch (ArgumentException erro)
            {
              Console.WriteLine("Erro: " + erro.Message);
            }

            break;
          }
        case 7:
          {
            Console.Write("Digite a matrícula: ");
            bool matriculavalida = int.TryParse(Console.ReadLine(), out int matricula);
            if (!matriculavalida)
            {
              Console.WriteLine("Digite uma matrícula válida!");
              break;
            }

            Console.Write("Digite o codigo: ");
            bool idvalido = int.TryParse(Console.ReadLine(), out int id);
            if (!idvalido)
            {
              Console.WriteLine("Digite um código válido!");
              break;
            }

            Console.Write("Digite a data de devolução (dd/MM/yyyy): ");
            string entradaData = Console.ReadLine() ?? "";

            DateTime? dataDevolucao;

            if (string.IsNullOrWhiteSpace(entradaData))
            {
              dataDevolucao = null;
            }
            else
            {
              bool dataValida = DateTime.TryParse(entradaData, out DateTime data);
              if (!dataValida)
              {
                Console.WriteLine("Data inválida!");
                break;
              }
              dataDevolucao = data;
            }

            try
            {
              biblioteca.RealizarDevolucao(matricula, id, dataDevolucao);
              Console.WriteLine("Devolução registrada com sucesso!");
            }
            catch (ArgumentException erro)
            {
              Console.WriteLine("Erro: " + erro.Message);
            }

            break;
          }
        case 8:
          {
            biblioteca.ExibirEmprestimosAtivos();
            break;
          }
        case 9:
          {
            biblioteca.GerarRelatorio();
            break;
          }
        default:
          Console.WriteLine("Opção inexistente! Escolha um número entre 0 e 9.");
          break;
      }

      if (continuar)
      {
        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadLine();
      }


    } while (continuar == true);

  }
}
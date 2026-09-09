# 📚 Sistema de Biblioteca Escolar

Aplicação de console em C# para controle de acervo, usuários e empréstimos de uma biblioteca escolar. Projeto acadêmico desenvolvido para a disciplina de Linguagem de Programação II (IFMG).

## 💡 Sobre o projeto

O sistema simula o funcionamento de uma biblioteca que ainda registrava seus empréstimos manualmente. A aplicação roda inteiramente no terminal, com os dados mantidos em memória durante a execução, e permite:

- Cadastrar usuários e materiais (livros e revistas)
- Consultar disponibilidade de materiais
- Realizar e registrar empréstimos e devoluções
- Calcular multas por atraso automaticamente
- Gerar relatórios com estatísticas do acervo

## 🛠️ Tecnologias e conceitos aplicados

- **C# / .NET** (console application)
- Tipos de dados, validação de entrada e saída
- Estruturas de decisão e repetição
- Coleções: `List` e `Dictionary`
- Programação Orientada a Objetos:
  - **Encapsulamento** — controle de acesso a estados internos (ex.: disponibilidade de um material só muda através de métodos específicos)
  - **Herança** — `Livro` e `Revista` herdam de `MaterialBiblioteca`
  - **Polimorfismo** — cálculo de prazo e multa implementado de forma diferente em cada subtipo, via `override`
  - **Interfaces** — contrato `IEmprestavel` implementado pela classe abstrata `MaterialBiblioteca`
  - **Classe abstrata** — `MaterialBiblioteca` define o comportamento comum entre os tipos de material

## 📂 Estrutura do projeto

```
BibliotecaEscolar/
├── Usuario.cs           # Representa o usuário e valida seus dados
├── IEmprestavel.cs       # Contrato de empréstimo (Emprestar/Devolver)
├── MaterialBiblioteca.cs # Classe abstrata base para materiais
├── Livro.cs              # Material com prazo de 7 dias e multa de R$1,50/dia
├── Revista.cs            # Material com prazo de 3 dias e multa de R$1,00/dia
├── Emprestimo.cs         # Controla datas, atraso e cálculo de multa
├── Biblioteca.cs         # Regras de negócio: cadastros, buscas, empréstimos, relatórios
├── Program.cs            # Menu interativo no console
└── BibliotecaEscolar.csproj
```

## ⚙️ Regras de negócio

- Matrícula de usuário e código de material devem ser únicos
- Um material emprestado não pode ser emprestado novamente
- Cada usuário pode ter no máximo 3 empréstimos ativos simultaneamente
- **Livro:** prazo de 7 dias, multa de R$ 1,50/dia de atraso
- **Revista:** prazo de 3 dias, multa de R$ 1,00/dia de atraso
- Entradas numéricas são validadas com `TryParse`, sem interromper o programa em caso de erro

## ▶️ Como executar

Pré-requisito: [.NET SDK](https://dotnet.microsoft.com/download) 8.0.100 instalado.

```bash
git clone https://github.com/seu-usuario/biblioteca-escolar.git
cd biblioteca-escolar
dotnet run
```

## 🖥️ Menu

```
===== BIBLIOTECA ESCOLAR =====
1 - Cadastrar usuário       6 - Realizar empréstimo
2 - Cadastrar material      7 - Registrar devolução
3 - Listar usuários         8 - Exibir empréstimos ativos
4 - Listar materiais        9 - Exibir relatório
5 - Consultar por código    0 - Sair
```

## 📌 Status

Projeto acadêmico concluído, com testes manuais realizados cobrindo cadastros, regras de empréstimo, cálculo de multas e casos de erro.

---

Desenvolvido como atividade prática da disciplina de Linguagem de Programação II - IFMG.

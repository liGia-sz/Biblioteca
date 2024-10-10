using System;

class Biblioteca
{
    static int limiteEmprestimos = 3; // Limite de empréstimos
    static int livrosEmprestados = 0; // Contador de livros emprestados
    static bool isAdmin; // Variável para verificar se o usuário é administrador
    static Livro[] livros = new Livro[100]; // Array para armazenar até 100 livros
    static int[] quantidadeDisponivel = new int[100]; // Array para controle de quantidade disponível
    static int contadorLivros = 0; // Contador de livros cadastrados

    static void Main()
    {
        IdentificarUsuario(); // Chama a função para identificar o tipo de usuário

        bool sair = false;

        while (!sair)
        {
            Console.WriteLine("SISTEMA DE GERENCIAMENTO DE BIBLIOTECA");
            MostrarMenu(); // Chama a função para mostrar o menu
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "0":
                    if (isAdmin) // Verifica se o usuário é admin
                    {
                        Console.WriteLine("\n CADASTRO DE LIVROS");
                        CadastrarLivro();
                    }
                    else
                    {
                        Console.WriteLine("Você não tem permissão para cadastrar livros.");
                    }
                    break;

                case "1":
                    Console.WriteLine("\n DEVOLUÇÃO DE LIVROS");
                    DevolverLivro();
                    break;

                case "2":
                    Console.WriteLine("\n EMPRÉSTIMO DE LIVROS");
                    if (livrosEmprestados < limiteEmprestimos) // Enquanto usuário pegou menos de 3 livros 
                    {
                        Console.Write("Título do livro que deseja pegar emprestado: ");
                        string titulo = Console.ReadLine();
                        for (int indiceLivro = 0; indiceLivro < contadorLivros; indiceLivro++)
                        {
                            if (livros[indiceLivro] != null && livros[indiceLivro].Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase) && quantidadeDisponivel[indiceLivro] > 0)
                            {
                                // Atualiza a quantidade disponível e contador de livros emprestados
                                quantidadeDisponivel[indiceLivro]--;
                                livrosEmprestados++;
                                Console.WriteLine($"Você pegou '{titulo}' emprestado.");
                                return;
                            }
                        }
                        Console.WriteLine("Livro não encontrado ou não disponível.");
                    }
                    else
                    {
                        Console.WriteLine("Limite de empréstimos atingido. Devolva um livro antes de pegar outro.");
                    }
                    break;

                case "3":
                    Console.WriteLine("\n NOSSO CATÁLOGO");
                    ConsultarCatalogo();
                    break;

                case "4":
                    sair = true;
                    break;

                default:
                    Console.WriteLine("Opção inválida! Tente novamente.");
                    break;
            }
        }
    }

    static void IdentificarUsuario()
    {
        Console.Write("Você é um administrador? (s/n): ");
        string resposta = Console.ReadLine().ToLower();
        isAdmin = resposta == "s"; // Define se o usuário é admin
    }

    static void MostrarMenu()
    {
        Console.WriteLine("\n MENU PRINCIPAL:");
        if (isAdmin)
        {
            Console.WriteLine("0. Cadastrar Livro");
        }
        Console.WriteLine("1. Devolver Livro");
        Console.WriteLine("2. Pegar Livro Emprestado");
        Console.WriteLine("3. Consultar Catálogo");
        Console.WriteLine("4. Sair");
        Console.Write("Escolha uma opção: ");
    }

    static void CadastrarLivro()
    {
        if (contadorLivros < livros.Length)
        {
            Console.Write("- Título do livro: ");
            string titulo = Console.ReadLine();
            Console.Write("- Autor do livro: ");
            string autor = Console.ReadLine();
            Console.Write("- Gênero do livro: ");
            string genero = Console.ReadLine();
            Console.Write("- Quantidade do livro: ");
            int quantidade = int.Parse(Console.ReadLine());

            // Criação do livro e adição ao array
            livros[contadorLivros] = new Livro(titulo, autor, genero);
            quantidadeDisponivel[contadorLivros] = quantidade; // Armazenar quantidade disponível
            contadorLivros++;
            Console.WriteLine("Livro cadastrado com sucesso!");
        }
    }

    static void DevolverLivro()
    {
        Console.WriteLine("Digite o nome do livro que deseja devolver:");
        string livroDevolvido = Console.ReadLine();
        
        for (int indiceLivro = 0; indiceLivro < contadorLivros; indiceLivro++)
        {
            if (livros[indiceLivro] != null && livros[indiceLivro].Titulo.Equals(livroDevolvido, StringComparison.OrdinalIgnoreCase))
            {
                quantidadeDisponivel[indiceLivro]++; // Incrementa a quantidade disponível
                livrosEmprestados--; // Decrementa o contador de livros emprestados
                Console.WriteLine($"O livro '{livroDevolvido}' foi devolvido com sucesso.");
                return;
            }
        }
        Console.WriteLine("Livro não encontrado.");
    }

    static void ConsultarCatalogo()
    {
        Console.WriteLine("Consultar Catálogo");
        for (int indiceLivro = 0; indiceLivro < contadorLivros; indiceLivro++)
        {
            if (livros[indiceLivro] != null)
            {
                Console.WriteLine($"Título: {livros[indiceLivro].Titulo}, Autor: {livros[indiceLivro].Autor}, Gênero: {livros[indiceLivro].Genero}, Quantidade disponível: {quantidadeDisponivel[indiceLivro]}");
            }
        }
    }
}

// Classe para representar um Livro
class Livro
{
    public string Titulo { get; }
    public string Autor { get; }
    public string Genero { get; }

    public Livro(string titulo, string autor, string genero)
    {
        Titulo = titulo;
        Autor = autor;
        Genero = genero;
    }
}

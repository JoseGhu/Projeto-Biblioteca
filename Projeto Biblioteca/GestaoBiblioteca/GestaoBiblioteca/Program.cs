using System;
using System.Collections.Generic;

namespace GestaoBiblioteca
{
    public class Livro
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public bool Emprestado { get; private set; }

        public Livro(string titulo, string autor)
        {
            Titulo = titulo;
            Autor = autor;
            Emprestado = false;
        }

        public void Emprestar()
        {
            Emprestado = true;
        }

        public void Devolver()
        {
            Emprestado = false;
        }
    }

    public class Usuario
    {
        public string Nome { get; set; }
        public List<Livro> LivrosEmprestados { get; private set; }

        public Usuario(string nome)
        {
            Nome = nome;
            LivrosEmprestados = new List<Livro>();
        }

        public void EmprestarLivro(Livro livro)
        {
            LivrosEmprestados.Add(livro);
            livro.Emprestar();
        }

        public void DevolverLivro(Livro livro)
        {
            LivrosEmprestados.Remove(livro);
            livro.Devolver();
        }
    }

    public class Biblioteca
    {
        public string Nome { get; set; }
        public List<Livro> Livros { get; private set; }

        public Biblioteca(string nome)
        {
            Nome = nome;
            Livros = new List<Livro>();
        }

        public void AdicionarLivro(Livro livro)
        {
            Livros.Add(livro);
        }

        public void ListarLivros()
        {
            Console.WriteLine($"Livros na biblioteca '{Nome}':");
            foreach (var livro in Livros)
            {
                string status = livro.Emprestado ? "Emprestado" : "Disponível";
                Console.WriteLine($"- {livro.Titulo} ({status})");
            }
        }

        public Livro BuscarLivro(string titulo)
        {
            return Livros.Find(l => l.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Biblioteca biblioteca = new Biblioteca("Biblioteca Central");
            Usuario usuario = new Usuario("GuGu");

            biblioteca.AdicionarLivro(new Livro("O Senhor dos Anéis", "J.R.R. Tolkien"));
            biblioteca.AdicionarLivro(new Livro("1984", "George Orwell"));
            biblioteca.AdicionarLivro(new Livro("Harry Potter A Câmara secreta", "J.K.R"));

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Sistema de Biblioteca ===");
                Console.WriteLine("1. Listar Livros");
                Console.WriteLine("2. Emprestar Livro");
                Console.WriteLine("3. Devolver Livro");
                Console.WriteLine("4. Sair");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        biblioteca.ListarLivros();
                        break;

                    case "2":
                        Console.Write("Digite o título do livro para emprestar: ");
                        string tituloEmprestar = Console.ReadLine();
                        var livroEmprestar = biblioteca.BuscarLivro(tituloEmprestar);

                        if (livroEmprestar == null)
                        {
                            Console.WriteLine("Livro não encontrado.");
                        }
                        else if (livroEmprestar.Emprestado)
                        {
                            Console.WriteLine("O livro já está emprestado.");
                        }
                        else
                        {
                            usuario.EmprestarLivro(livroEmprestar);
                            Console.WriteLine($"Livro '{tituloEmprestar}' emprestado com sucesso!");
                        }
                        break;

                    case "3":
                        Console.Write("Digite o título do livro para devolver: ");
                        string tituloDevolver = Console.ReadLine();
                        var livroDevolver = biblioteca.BuscarLivro(tituloDevolver);

                        if (livroDevolver == null || !usuario.LivrosEmprestados.Contains(livroDevolver))
                        {
                            Console.WriteLine("Você não tem este livro emprestado.");
                        }
                        else
                        {
                            usuario.DevolverLivro(livroDevolver);
                            Console.WriteLine($"Livro '{tituloDevolver}' devolvido com sucesso!");
                        }
                        break;

                    case "4":
                        Console.WriteLine("Encerrando o programa...");
                        return;

                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }

                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }
    }
}

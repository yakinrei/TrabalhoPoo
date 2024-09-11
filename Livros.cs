using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Projeto_Grupo_Sistema_de_Biblioteca_POO
{
    internal class Livros
    {
        private uint Id { get; set; }
        private string Titulo { get; set; }
        private string Autor { get; set; }
        private int Ano { get; set; }
        private int Copias { get; set; }
        public int CopiasEmprestadas { get; set; }


        public Livros() : this(Ft_VerificaTituloLivro(), Ft_VerificaNome(), Ft_VerificaAno(), Ft_VerificaNumeroCopias()) { }
        

        public Livros(string titulo, string autor, int ano, int copias)
        {
            Titulo = titulo;
            Autor = autor;
            Ano = ano;
            Copias = copias;
            CopiasEmprestadas = 0;
            Id = Bibliotecas.NextIdBook;
            Bibliotecas.NextIdBook++;
        }

        private static string Ft_VerificaTituloLivro()
        {
            string titulo;
            bool verifica = false;

            // Permite letras (acentuadas e sem acento), números, espaços e caracteres especiais
            Regex regex = new Regex(@"^[a-zA-ZÀ-ÖØ-öø-ÿ0-9\s,.!?;:'""\-()]+$");

            // Palavras que não devem ser capitalizadas no meio do título
            string[] palavrasMinusculas = { "de", "da", "do", "e", "das", "dos"};

            do
            {
                Console.Write("Insira o Título do Livro: ");
                titulo = Console.ReadLine();
                titulo = titulo.Trim();

                // Remover múltiplos espaços
                titulo = Regex.Replace(titulo, @"\s+", " ");

                // Verifica se o título é válido (não nulo, contém caracteres permitidos e tamanho adequado)
                verifica = !string.IsNullOrEmpty(titulo) && regex.IsMatch(titulo) && titulo.Length >= 5 && titulo.Length <= 100;

                if (!verifica)
                {
                    Console.Clear();
                    Console.WriteLine("Título inválido. O título deve conter letras, números, acentos e caracteres permitidos, com no mínimo 5 e no máximo 100 caracteres.");
                }
                else
                {
                    // Formatar o título
                    TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
                    string[] palavras = titulo.ToLower().Split(' ');

                    // Capitaliza cada palavra, exceto as da lista de palavrasMinusculas
                    for (int i = 0; i < palavras.Length; i++)
                    {
                        if (i == 0 || !Array.Exists(palavrasMinusculas, p => p == palavras[i]))
                        {
                            palavras[i] = textInfo.ToTitleCase(palavras[i]);
                        }
                    }

                    // Junta o título novamente
                    titulo = string.Join(" ", palavras);
                }

            } while (!verifica);

            return titulo;
        }


        private static string Ft_VerificaNome()
        {
            string nome;
            bool verifica = false;

            // Permite letras (com e sem acento) e espaços
            Regex regex = new Regex(@"^[a-zA-ZÀ-ÖØ-öø-ÿ\s]+$");

            // Lista de palavras comuns que devem ficar em minúsculo no meio do nome
            string[] palavrasMinusculas = { "de", "da", "do", "dos", "das" };

            do
            {
                Console.Write("Insira o Nome do Autor: ");
                nome = Console.ReadLine();
                nome = nome.Trim();

                // Remove múltiplos espaços entre palavras
                nome = Regex.Replace(nome, @"\s+", " ");

                // Verifica se o nome é válido (não nulo, contém apenas letras e espaços, e tem comprimento adequado)
                verifica = !string.IsNullOrEmpty(nome) && regex.IsMatch(nome) && nome.Length >= 3 && nome.Length <= 50;

                if (!verifica)
                {
                    Console.Clear();
                    Console.WriteLine("Nome inválido. O nome deve conter apenas letras e espaços, com no mínimo 3 e no máximo 50 caracteres.");
                }
                else
                {
                    // Formatar o nome
                    TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
                    string[] palavras = nome.ToLower().Split(' ');

                    // Capitaliza a primeira palavra e demais palavras exceto as da lista de palavrasMinusculas
                    for (int i = 0; i < palavras.Length; i++)
                    {
                        if (i == 0 || !Array.Exists(palavrasMinusculas, p => p == palavras[i]))
                        {
                            palavras[i] = textInfo.ToTitleCase(palavras[i]);
                        }
                    }

                    // Junta o nome novamente
                    nome = string.Join(" ", palavras);
                }

            } while (!verifica);

            return nome;
        }


        private static int Ft_VerificaAno()
        {
            int ano;
            bool verifica = false;

            do
            {
                Console.Write("Insira o ano (4 dígitos): ");
                string input = Console.ReadLine().Trim();

                // Verifica se o input é um número inteiro e tem 4 dígitos
                verifica = int.TryParse(input, out ano) && input.Length == 4;

                if (!verifica)
                {
                    Console.Clear();
                    Console.WriteLine("Ano inválido. O ano deve conter exatamente 4 dígitos.");
                }

            } while (!verifica);

            return ano;
        }

        private static int Ft_VerificaNumeroCopias()
        {
            int numeroCopias;
            bool verifica = false;

            do
            {
                Console.Write("Insira o número de cópias (máximo 99): ");
                string input = Console.ReadLine().Trim();

                // Verifica se o input é um número inteiro e está entre 1 e 99
                verifica = int.TryParse(input, out numeroCopias) && numeroCopias >= 1 && numeroCopias <= 99;

                if (!verifica)
                {
                    Console.Clear();
                    Console.WriteLine("Número inválido. O número de cópias deve ser entre 1 e 99.");
                }

            } while (!verifica);

            return numeroCopias;
        }

        public uint Ft_GetId() 
        { 
            return this.Id; 
        }
        public string Ft_GetTitulo()
        {
            return this.Titulo;
        }
        public int Ft_GetAno()
        {
            return this.Ano;
        }
        public string Ft_GetAutor()
        {
            return this.Autor;
        }
        public int Ft_GetCopias()
        {
            return this.Copias;
        }

        public void Ft_ExibirInformacoes()
        {
            Console.WriteLine($"Livro: {Ft_GetTitulo(),-42} Código do Livro: {Ft_GetId().ToString("D5"),5}");
        }

        public void Ft_FullExibirInformacoes()
        {
            Console.WriteLine($"Livro: {Ft_GetTitulo(),-42} Código do Livro: {Ft_GetId().ToString("D5"),5}");
            Console.WriteLine($"Autor: {Ft_GetAutor(),-42} {Ft_GetCopias()-CopiasEmprestadas} de {Ft_GetCopias()} disponiveis");
        }
    }
}

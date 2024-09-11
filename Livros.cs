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
        public uint Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int Ano { get; set; }
        public int Copias { get; set; }
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

            // Permite letras, números, espaços e caracteres especiais
            Regex regex = new Regex(@"^[a-zA-Z0-9\s,.!?;:'""\-()]+$");

            do
            {
                Console.Write("Insira o Título do Livro: ");
                titulo = Console.ReadLine();
                titulo = titulo.Trim();

                // Verifica se o título é válido (não nulo e contém apenas caracteres permitidos)
                verifica = !string.IsNullOrEmpty(titulo) && regex.IsMatch(titulo);

                if (!verifica)
                {
                    Console.Clear();
                    Console.WriteLine("Título inválido. O título deve conter apenas letras, números e caracteres permitidos.");
                }
                else
                {
                    // Formata o título: primeira letra de cada palavra maiúscula e restante minúscula
                    TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
                    titulo = textInfo.ToTitleCase(titulo.ToLower());
                }

            } while (!verifica);

            return titulo;
        }

        private static string Ft_VerificaNome()
        {
            string nome;
            bool verifica = false;
            Regex regex = new Regex(@"^[a-zA-Z\s]+$"); // Permite apenas letras e espaços

            do
            {
                Console.Write("Insira o Nome do Autor: ");
                nome = Console.ReadLine();
                nome = nome.Trim();

                // Verifica se o nome é válido (não nulo e não contém caracteres especiais)
                verifica = !string.IsNullOrEmpty(nome) && regex.IsMatch(nome);

                if (!verifica)
                {
                    Console.Clear();
                    Console.WriteLine("Nome Inválido. O nome deve conter apenas letras e espaços.");
                }
                else
                {
                    // Formata o nome: primeira letra de cada palavra maiúscula e restante minúscula
                    TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
                    nome = textInfo.ToTitleCase(nome.ToLower());
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

    }
}

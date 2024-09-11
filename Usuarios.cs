﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Projeto_Grupo_Sistema_de_Biblioteca_POO
{
    internal class Usuarios
    {
        private uint Id { get; set; }
        private string Nome { get; set; }
        private string Endereco { get; set; }
        private string Contato { get; set; }


        public Usuarios() : this(Ft_VerificaNome(), Ft_VerificaEndereco(), Ft_VerificaTelemovel()) { }

        public Usuarios(string nome, string endereco, string contato)
        {
            Nome = nome;
            Endereco = endereco;
            Contato = contato;
            Id = Bibliotecas.NextIdUsuario;
            Bibliotecas.NextIdUsuario++;
        }

        private static string Ft_VerificaNome()
        {
            string nome2;
            bool verifica = false;
            Regex regex = new Regex(@"^[a-zA-Z\s]+$"); // Permite apenas letras e espaços

            do
            {
                Console.Write("Insira o Nome do Usuário: ");
                nome2 = Console.ReadLine();
                nome2 = nome2.Trim();

                // Verifica se o nome é válido (não nulo e não contém caracteres especiais)
                verifica = !string.IsNullOrEmpty(nome2) && regex.IsMatch(nome2);

                if (!verifica)
                {
                    Console.Clear();
                    Console.WriteLine("Nome Inválido. O nome deve conter apenas letras e espaços.");
                }
                else
                {
                    // Formata o nome: primeira letra de cada palavra maiúscula e restante minúscula
                    TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
                    nome2 = textInfo.ToTitleCase(nome2.ToLower());
                }

            } while (!verifica);

            return nome2;
        }

        private static string Ft_VerificaEndereco()
        {
            string endereco;
            bool verifica = false;

            // Permite letras, números, espaços, vírgulas, pontos, hífens e barras
            Regex regex = new Regex(@"^[a-zA-Z0-9\s,.\-\/]+$");

            do
            {
                Console.Write("Insira o Endereço: ");
                endereco = Console.ReadLine();
                endereco = endereco.Trim();

                // Verifica se o endereço é válido (não nulo e não contém caracteres especiais inválidos)
                verifica = !string.IsNullOrEmpty(endereco) && regex.IsMatch(endereco);

                if (!verifica)
                {
                    Console.Clear();
                    Console.WriteLine("Endereço Inválido. O endereço deve conter apenas letras, números e caracteres permitidos (, . - /).");
                }
                else
                {
                    // Formata o endereço: primeira letra de cada palavra maiúscula, exceto artigos e preposições
                    TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
                    endereco = textInfo.ToTitleCase(endereco.ToLower());

                    // Exceções para palavras comuns em endereços que não devem ser capitalizadas
                    string[] excecoes = { "de", "do", "da", "das", "dos", "e", "a", "à", "o", "os" };
                    foreach (string palavra in excecoes)
                    {
                        endereco = Regex.Replace(endereco, $@"\b{palavra}\b", palavra, RegexOptions.IgnoreCase);
                    }
                }
            } while (!verifica);
            return endereco;
        }

        private static string Ft_VerificaTelemovel()
        {
            string telemovel;
            bool verifica = false;

            // Expressão regular para verificar se o número tem 9 dígitos e começa com 9
            Regex regex = new Regex(@"^9\d{8}$");

            do
            {
                Console.Write("Insira o número de telemóvel: ");
                telemovel = Console.ReadLine().Trim();

                // Verifica se o telemóvel é válido
                verifica = regex.IsMatch(telemovel);

                if (!verifica)
                {
                    Console.Clear();
                    Console.WriteLine("Número de telemóvel inválido. Deve conter 9 dígitos e começar com 9.");
                }

            } while (!verifica);
            return telemovel;
        }

        public string Ft_GetNome() 
        { 
            return this.Nome; 
        }
        public uint Ft_GetId() 
        { 
            return this.Id; 
        }
        public string Ft_GetContato() 
        {
            return this.Contato; 
        }
        public string Ft_GetMorada() 
        { 
            return this.Endereco; 
        }
        public void Ft_ExibirInformacoes()
        {
            Console.WriteLine($"Usuário: {Ft_GetNome(),-40} Nº Usuário: {Ft_GetId().ToString("D5"),10}");
        }
        public void Ft_FullExibirInformacoes()
        {
            Console.WriteLine($"Usuário: {Ft_GetNome(),-40} Nº Usuário: {Ft_GetId().ToString("D5"),10}");
            Console.WriteLine($"Morada: {Ft_GetMorada()}");
            Console.WriteLine($"Contato: {Ft_GetContato()}");
        }
    }
}

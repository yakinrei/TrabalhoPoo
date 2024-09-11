using System;
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
            string nome;
            bool verifica = false;

            // Permite letras (com e sem acento) e espaços
            Regex regex = new Regex(@"^[a-zA-ZÀ-ÖØ-öø-ÿ\s]+$");

            // Lista de palavras comuns que devem ficar em minúsculo no meio do nome
            string[] palavrasMinusculas = { "de", "da", "do", "dos", "das" };

            do
            {
                Console.Write("Insira o Nome do Utente: ");
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


        private static string Ft_VerificaEndereco()
        {
            string endereco;
            bool verifica = false;

            // Adicionar caracteres especiais usados no português
            Regex regex = new Regex(@"^[a-zA-Z0-9\s,.\-\/áéíóúç]+$");

            // Validação do código postal no formato NNNN-NNN
            Regex regexPostal = new Regex(@"\d{4}-\d{3}");

            do
            {
                // 1. Solicitar o endereço e remover espaços extras
                endereco = Console.ReadLine().Trim();

                // 2. Verificar se o endereço é válido e contém um código postal
                verifica = !string.IsNullOrEmpty(endereco)
                           && regex.IsMatch(endereco)
                           && regexPostal.IsMatch(endereco);

                // 3. Se inválido, mostrar mensagem de erro
                if (!verifica)
                {
                    Console.WriteLine("Endereço Inválido. Deve conter apenas letras, números, caracteres permitidos, e um código postal no formato NNNN-NNN.");
                }
                else
                {
                    // 4. Formatar o endereço (primeiras letras maiúsculas, exceções)
                    TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
                    endereco = textInfo.ToTitleCase(endereco.ToLower());

                    // Exceções para palavras que não devem ser capitalizadas
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

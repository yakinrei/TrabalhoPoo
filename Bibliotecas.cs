using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Projeto_Grupo_Sistema_de_Biblioteca_POO
{
    internal class Bibliotecas
    {
        #region Atributos
        public static uint NextIdUsuario = 1;
        public static uint NextIdBook = 1;
        public static string currentDirectory = Environment.CurrentDirectory;
        public static string projectDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(currentDirectory).FullName).FullName).FullName;
        public List<Livros> Acervo = new List<Livros>();
        private List<Usuarios> Utentes = new List<Usuarios>();
        private List<Emprestimos> EmprestimosAtivos = new List<Emprestimos>();
        public string name;
        #endregion

        #region Builder
        public Bibliotecas() : this(Ft_VerificaNome()) { }
        public Bibliotecas(string nome)
        {
            name = nome;
            if (!Directory.Exists(projectDirectory + $"\\{nome}"))
            {
                Directory.CreateDirectory(projectDirectory + $"\\{nome}");
                Console.Clear();
                Console.WriteLine("Criando nova Biblioteca");
                Ft_RegistrarLivro("ABC Diarreia", "Hernani Roxo", 2024, 3);
                Ft_RegistrarLivro("Guia Prático de Ginecologia", "Mariana Souza", 2023, 2);
                Ft_RegistrarLivro("Fundamentos de Oncologia", "Pedro Matos", 2022, 4);
                Ft_RegistrarLivro("Manual de Doenças Infecciosas", "Carla Nunes", 2024, 5);
                Ft_RegistrarLivro("Introdução à Neurologia", "João Ribeiro", 2023, 3);
                Ft_RegistrarLivro("Patologia Cardiovascular", "Ana Martins", 2022, 1);
                Ft_RegistrarLivro("Diagnóstico em Dermatologia", "Bruno Lopes", 2024, 2);
                Ft_RegistrarLivro("Terapia Intensiva", "Luiza Azevedo", 2002, 6);
                Ft_RegistrarLivro("Farmacologia Geral", "Jorge Pinto", 2023, 3);
                Ft_RegistrarLivro("Exames de Imagem", "Silvia Almeida", 2024, 4);
                Ft_RegistrarUtente("Pedro Almeida", "Rua do Couteiro 72 R/C 4705-091 Braga", "911510784");
                Ft_RegistrarUtente("Maria Silva", "Rua de São Vicente 15 1º 4700-470 Braga", "919876543");
                Ft_RegistrarUtente("João Pereira", "Rua dos Combatentes 23 2º 4710-000 Braga", "917654321");
                Ft_RegistrarUtente("Ana Costa", "Avenida da Liberdade 100 3º 4700-222 Braga", "912345678");
                Ft_RegistrarUtente("Tiago Fernandes", "Rua da Boavista 12 4705-300 Braga", "915678432");
                Ft_RegistrarUtente("Carla Sousa", "Largo da Sé 5 4700-443 Braga", "916543210");
                Ft_RegistrarUtente("Luís Santos", "Rua do Raio 50 4700-920 Braga", "913214365");
                Ft_RegistrarUtente("Sara Oliveira", "Rua da Misericórdia 7 4705-210 Braga", "918765432");
                Ft_RegistrarUtente("Ricardo Gonçalves", "Rua da Estação 9 4700-550 Braga", "914326587");
                Ft_RegistrarUtente("Filipa Martins", "Praça da República 2 4705-120 Braga", "919654321");
                Ft_ProcessaEmprestimo(Ft_LocalizaLivro(3), Ft_LocalizaUtente(7), DateTime.Parse("2024-08-30"));
                Ft_ProcessaEmprestimo(Ft_LocalizaLivro(9), Ft_LocalizaUtente(7), DateTime.Parse("2024-08-31"));
                Ft_ProcessaEmprestimo(Ft_LocalizaLivro(5), Ft_LocalizaUtente(3), DateTime.Parse("2024-09-01"));
                Ft_ProcessaEmprestimo(Ft_LocalizaLivro(6), Ft_LocalizaUtente(2), DateTime.Parse("2024-09-02"));
                Ft_ProcessaEmprestimo(Ft_LocalizaLivro(7), Ft_LocalizaUtente(2), DateTime.Parse("2024-09-03"));
                Ft_ProcessaEmprestimo(Ft_LocalizaLivro(2), Ft_LocalizaUtente(1), DateTime.Parse("2024-09-04"));
                Ft_ProcessaEmprestimo(Ft_LocalizaLivro(7), Ft_LocalizaUtente(5), DateTime.Parse("2024-09-05"));
                Ft_ProcessaEmprestimo(Ft_LocalizaLivro(1), Ft_LocalizaUtente(6), DateTime.Parse("2024-09-06"));
                Ft_ProcessaEmprestimo(Ft_LocalizaLivro(4), Ft_LocalizaUtente(6), DateTime.Parse("2024-09-07"));
                Ft_ProcessaEmprestimo(Ft_LocalizaLivro(7), Ft_LocalizaUtente(6), DateTime.Parse("2024-09-08"));
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Carregando dados da Biblioteca {nome}");
                Ft_CarregaAcervo();
                Console.Clear();
                Ft_CarregaUsuarios();
                Console.Clear();
                Ft_CarregaEmprestimos();
                Console.Clear();
            }
        }
        private static string Ft_VerificaNome()
        {
            string nome;
            bool verifica = false;
            Regex regex = new Regex(@"^[^\\\/:*?""<>|]+$");

            do
            {
                Console.Write("Insira o Nome da Biblioteca: ");
                nome = Console.ReadLine();
                Console.Clear();
                nome = nome.Trim();

                // Verifica se o nome é válido (não nulo e não contém caracteres especiais)
                verifica = !string.IsNullOrEmpty(nome) && regex.IsMatch(nome);

                if (!verifica)
                {
                    Console.Clear();
                    Console.WriteLine("Nome Inválido. O nome deve conter apenas letras.");
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

        public void Ft_CarregaAcervo()
        {
            string filePath = projectDirectory + $"\\{this.name}\\acervo.txt";

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string linha;
                    int i = 0;
                    string titulo = "";
                    string autor = "";
                    int ano = 0;
                    int copias = 0;
                    while ((linha = reader.ReadLine()) != null)
                    {
                        if (i == 0)
                        {
                            titulo = linha;
                            i++;
                        }
                        else if (i == 1)
                        {
                            autor = linha;
                            i++;
                        }
                        else if (i == 2)
                        {
                            ano = int.Parse(linha);
                            i++;
                        }
                        else if (i == 3)
                        {
                            copias = int.Parse(linha);
                            Ft_RegistrarLivro(titulo, autor, ano, copias);
                            i = 0;
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"O arquivo {filePath} não existe.");
            }

            return;
        }
        public void Ft_CarregaUsuarios()
        {

            string filePath = projectDirectory + $"\\{this.name}\\utentes.txt";

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string linha;
                    int i = 0;
                    string nome = "";
                    string endereco = "";
                    string contato = "";
                    while ((linha = reader.ReadLine()) != null)
                    {
                        if (i == 0)
                        {
                            nome = linha;
                            i++;
                        }
                        else if (i == 1)
                        {
                            endereco = linha;
                            i++;
                        }
                        else if (i == 2)
                        {
                            contato = linha;
                            Ft_RegistrarUtente(nome, endereco, contato);
                            i = 0;
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"O arquivo {filePath} não existe.");
            }

            return;
        }
        public void Ft_CarregaEmprestimos()
        {
            string filePath = projectDirectory + $"\\{this.name}\\emprestimos.txt";

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string linha;
                    int i = 0;
                    Livros livro = null;
                    Usuarios utente = null;
                    DateTime DataEmprestimo = DateTime.Parse("01/01/2000");
                    while ((linha = reader.ReadLine()) != null)
                    {
                        if (i == 0)
                        {
                            utente = Ft_LocalizaUtente(uint.Parse(linha));
                            i++;
                        }
                        else if (i == 1)
                        {
                            livro = Ft_LocalizaLivro(uint.Parse(linha));
                            i++;
                        }
                        else if (i == 2)
                        {
                            DataEmprestimo = DateTime.Parse(linha);
                            Ft_ProcessaEmprestimo(livro, utente, DataEmprestimo);
                            i = 0;
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"O arquivo {filePath} não existe.");
            }
            return;
        }
        public void Ft_SalvaDados()
        {
            string filePath = projectDirectory + $"\\{this.name}\\acervo.txt";
            byte[] linha;
            File.Delete(filePath);
            using (FileStream fs = File.Create(filePath))
            {
                foreach (var item in Acervo)
                {
                    linha = new UTF8Encoding(true).GetBytes(item.Ft_GetTitulo() + "\n");
                    fs.Write(linha, 0, linha.Length);
                    linha = new UTF8Encoding(true).GetBytes(item.Ft_GetAutor() + "\n");
                    fs.Write(linha, 0, linha.Length);
                    linha = new UTF8Encoding(true).GetBytes(item.Ft_GetAno().ToString() + "\n");
                    fs.Write(linha, 0, linha.Length);
                    linha = new UTF8Encoding(true).GetBytes(item.Ft_GetCopias().ToString() + "\n");
                    fs.Write(linha, 0, linha.Length);
                }
            }
            filePath = projectDirectory + $"\\{this.name}\\utentes.txt";
            File.Delete(filePath);
            using (FileStream fs = File.Create(filePath))
            {
                foreach (var item in Utentes)
                {
                    linha = new UTF8Encoding(true).GetBytes(item.Ft_GetNome() + "\n");
                    fs.Write(linha, 0, linha.Length);
                    linha = new UTF8Encoding(true).GetBytes(item.Ft_GetMorada() + "\n");
                    fs.Write(linha, 0, linha.Length);
                    linha = new UTF8Encoding(true).GetBytes(item.Ft_GetContato() + "\n");
                    fs.Write(linha, 0, linha.Length);
                }
            }
            filePath = projectDirectory + $"\\{this.name}\\emprestimos.txt";
            File.Delete(filePath);
            using (FileStream fs = File.Create(filePath))
            {
                foreach (var item in EmprestimosAtivos)
                {
                    linha = new UTF8Encoding(true).GetBytes(item.Ft_GetUsuario().Ft_GetId().ToString() + "\n");
                    fs.Write(linha, 0, linha.Length);
                    linha = new UTF8Encoding(true).GetBytes(item.Ft_GetLivro().Ft_GetId().ToString() + "\n");
                    fs.Write(linha, 0, linha.Length);
                    linha = new UTF8Encoding(true).GetBytes(item.Ft_GetDataEmprestimo().Year + "-" + (item.Ft_GetDataEmprestimo().Month >= 10 ? item.Ft_GetDataEmprestimo().Month : "0" + item.Ft_GetDataEmprestimo().Month) + "-" + (item.Ft_GetDataEmprestimo().Day >= 10 ? item.Ft_GetDataEmprestimo().Day : "0" + item.Ft_GetDataEmprestimo().Day) + "\n");
                    fs.Write(linha, 0, linha.Length);
                }
            }
            Console.WriteLine("Obrigado pela preferência");
            return;
        }
        #endregion

        #region Localização
        public Livros Ft_GetLivro()
        {
            Console.WriteLine("Insira o Título ou ID do livro");
            string x = Console.ReadLine();
            uint id;
            Console.Clear();
            Livros livro = null;
            bool verifica = uint.TryParse((x), out id);
            if (!verifica)
                livro = Ft_LocalizaLivro(x);
            else
                livro = Ft_LocalizaLivro(id);
            return livro;
        }

        public Usuarios Ft_GetUtente()
        {
            Console.WriteLine("Insira o Nome ou ID do utente");
            string x = Console.ReadLine();
            Console.Clear();
            uint id;
            Usuarios solicitante = null;
            bool verifica = uint.TryParse((x), out id);
            if (!verifica)
                solicitante = Ft_LocalizaUtente(x);
            else
                solicitante = Ft_LocalizaUtente(id);
            return solicitante;
        }

        public Usuarios Ft_LocalizaUtente(uint idusuario)
        {
            Usuarios solicitante = null;

            foreach (var item in Utentes)
            {
                if (item.Ft_GetId() == idusuario)
                {
                    solicitante = item;
                    break;
                }
            }
            return (solicitante);
        }

        public Usuarios Ft_LocalizaUtente(string usuario)
        {
            Usuarios solicitante = null;

            foreach (var item in Utentes)
            {
                if (item.Ft_GetNome() == usuario)
                {
                    solicitante = item;
                    break;
                }
            }
            return (solicitante);
        }

        public Livros Ft_LocalizaLivro(uint idlivro)
        {
            Livros emprestado = null;

            foreach (var item in Acervo)
            {
                if (item.Ft_GetId() == idlivro)
                {
                    emprestado = item;
                    break;
                }
            }
            return (emprestado);
        }

        public Livros Ft_LocalizaLivro(string titulo)
        {
            Livros emprestado = null;

            foreach (var item in Acervo)
            {
                if (item.Ft_GetTitulo() == titulo)
                {
                    emprestado = item;
                    break;
                }
            }
            return (emprestado);
        }

        #endregion

        #region Registros
        public void Ft_RegistrarLivro(string titulo, string autor, int ano, int copias)
        {
            Acervo.Add(new Livros(titulo, autor, ano, copias));

            //Console.WriteLine($"Livro '{titulo}' registrado.\n");
        }

        public void Ft_RegistrarLivro()
        {
            Acervo.Add(new Livros());

            Console.WriteLine($"Livro '{Acervo[Acervo.Count - 1].Ft_GetTitulo()}' registrado.\n");
        }

        public void Ft_RegistrarUtente(string nome, string endereco, string contato)
        {
            Utentes.Add(new Usuarios(nome, endereco, contato));
            //Console.WriteLine($"Utente '{nome}' registrado.\n");
        }

        public void Ft_RegistrarUtente()
        {
            Utentes.Add(new Usuarios());
            Console.WriteLine($"Utente '{Utentes[Utentes.Count - 1].Ft_GetNome()}' registrado.\n");
        }
        #endregion
        
        #region Emprestimos
        public bool Ft_VerificarDisponibilidade(Livros livro)
        {
            if (livro == null)
            {
                Console.WriteLine("Livro não disponível");
                return false;
            }
            else if (livro.Ft_GetCopias() > livro.Ft_GetCopiasEmprestadas())
            {
                Console.WriteLine($"Existem {livro.Ft_GetCopias() - livro.Ft_GetCopiasEmprestadas()} cópias do livro {livro.Ft_GetTitulo()} disponiveis");
                return true;
            }
            else
            {
                Console.WriteLine("Livro não disponível");
                return false;
            }
        }

        public void Ft_EmprestarLivro()
        {
            Usuarios solicitante = Ft_GetUtente();
            Livros requerido = Ft_GetLivro();

            if (solicitante == null)
            {
                Console.WriteLine("Usuário não encontrado");
                return;
            }
            else if (requerido == null)
            {
                Console.WriteLine("Livro não encontrado");
                return;
            }
            else
            {
                if (Ft_VerificarDisponibilidade(requerido))
                {
                    EmprestimosAtivos.Add(new Emprestimos(requerido, solicitante));
                    requerido.Ft_EmprestaLivro();
                    Console.WriteLine($"Livro '{requerido.Ft_GetTitulo()}' emprestado para {solicitante.Ft_GetNome()}.\n");
                    return;
                }
            }
            Console.WriteLine($"Livro: {requerido.Ft_GetTitulo()} não está disponível.\n");
        }

        public void Ft_ProcessaEmprestimo(Livros livro, Usuarios usuario, DateTime DT)
        {
            EmprestimosAtivos.Add(new Emprestimos(livro, usuario, DT));
            livro.Ft_EmprestaLivro();
            //Console.WriteLine($"Livro '{livro.Titulo}' emprestado para {usuario.Nome}.\n");
            return;
        }
        
        public void Ft_RetornarLivro()
        {
            Usuarios solicitante = Ft_GetUtente();
            Livros devolvido = Ft_GetLivro();
            Emprestimos Devolucao = null;
            if (solicitante == null)
            {
                Console.WriteLine("Usuário não encontrado");
                return;
            }
            else if (devolvido == null)
            {
                Console.WriteLine("Livro não encontrado");
                return;
            }
            else
            {
                foreach (var item in EmprestimosAtivos)
                {
                    if (item.Ft_GetLivro() == devolvido && item.Ft_GetUsuario() == solicitante)
                    {
                        devolvido.Ft_LivroDevolvido();
                        Devolucao = item;
                        Console.WriteLine($"Livro '{devolvido.Ft_GetTitulo()}' devolvido por {solicitante.Ft_GetNome()}.\n");
                        break;
                    }
                }
                if (Devolucao != null)
                {
                    EmprestimosAtivos.Remove(Devolucao);
                }
                else
                {
                    Console.WriteLine($"Usuário {solicitante.Ft_GetNome()} não se encontra em posse do livro '{devolvido.Ft_GetTitulo()}'.\n");
                }
            }
        }

        #endregion

        #region Relatorios
        public void Ft_GerarRelatoriodeEmprestimo()
        {
            Usuarios solicitante = Ft_GetUtente();

            if (solicitante == null)
            {
                Console.WriteLine("Usuário não encontrado");
                return;
            }

            Console.WriteLine("Relatório de Empréstimo:");
            solicitante.Ft_ExibirInformacoes();

            bool emprestado = false;
            foreach (var item in EmprestimosAtivos)
            {
                TimeSpan Dias = DateTime.Now - item.Ft_GetDataEmprestimo();
                if (solicitante == item.Ft_GetUsuario())
                {
                    item.Ft_ExibirInformacoesLivro();
                    Console.WriteLine($"{(Dias.Days > 7 ?" - EM ATRASO" : "")}");
                    emprestado = true;
                }
            }
            if (!emprestado)
            {
                Console.WriteLine("Nenhum livro para devolver");
            }
            Console.WriteLine("");
        }

        public void Ft_GerarRelatoriodeEmprestimo2()
        {
            Livros relatado = Ft_GetLivro();

            if (relatado == null)
            {
                Console.WriteLine("Livro não encontrado");
                return;
            }

            Console.WriteLine("Verificando Disponibilidade:");
            relatado.Ft_FullExibirInformacoes();
            bool emprestado = false;
            foreach (var item in EmprestimosAtivos)
            {
                if (relatado == item.Ft_GetLivro())
                {
                    item.Ft_ExibirInformacoesUsuario();
                    emprestado = true;
                }
            }
            if (!emprestado)
            {
                Console.WriteLine("Nenhum livro para devolver");
            }
            Console.WriteLine("");
        }

        public void Ft_GerarRelatoriodeEmprestimo3()
        {
            Console.WriteLine("Relatório de Empréstimos Vencidos (mais de 7 dias):");
            Console.WriteLine("");
            bool emprestado = false;
            foreach (var item in EmprestimosAtivos)
            {
                TimeSpan diferencaDias = DateTime.Now - item.Ft_GetDataEmprestimo();

                if (diferencaDias.Days > 7)
                {
                    item.Ft_ExibirInformacoesAtraso();
                    emprestado = true;
                }
            }
            if (!emprestado)
            {
                Console.WriteLine("Nenhum empréstimo venceu o prazo de 7 dias.");
            }
            Console.WriteLine("");
        }

        #endregion

        #region Menu
        public uint Ft_ExibirMenu()
        {
            uint comando;
            Console.WriteLine($"[1] Registrar Livro");
            Console.WriteLine($"[2] Registrar Usuário");
            Console.WriteLine($"[3] Verificar Disponibilidade");
            Console.WriteLine($"[4] Emprestar Livro");
            Console.WriteLine($"[5] Devolução de Livro");
            Console.WriteLine($"[6] Relatório de Utente");
            Console.WriteLine($"[7] Relatório de Atrasos");
            Console.WriteLine($"[8] Encerrar Programa");
            bool verifica = uint.TryParse(Console.ReadLine(), out comando);
            Console.Clear();
            if (!verifica || comando < 1 || comando > 8)
            {
                Console.WriteLine("Comando Inválido");
                comando = 0;
            }
            return comando;
        }
        #endregion

    }
}

namespace Projeto_Grupo_Sistema_de_Biblioteca_POO
{
        internal class Program
        {
            static void Main(string[] args)
            {
                Bibliotecas Biblioteca = new Bibliotecas();
                uint comando = 0;
                Console.Clear();
                do
                {
                    comando = Biblioteca.Ft_ExibirMenu();
                    if (comando == 0)
                        continue;
                    else if (comando == 1)
                        Biblioteca.Ft_RegistrarLivro();
                    else if (comando == 2)
                        Biblioteca.Ft_RegistrarUtente();
                    else if (comando == 3)
                        Biblioteca.Ft_GerarRelatoriodeEmprestimo2();
                    else if (comando == 4)
                        Biblioteca.Ft_EmprestarLivro();
                    else if (comando == 5)
                        Biblioteca.Ft_RetornarLivro();
                    else if (comando == 6)
                        Biblioteca.Ft_GerarRelatoriodeEmprestimo();
                    else if (comando == 7)
                        Biblioteca.Ft_GerarRelatoriodeEmprestimo3();
                    else
                        Biblioteca.Ft_SalvaDados();
                }
                while (comando != 8);


            }
        }


}

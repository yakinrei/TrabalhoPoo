using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Grupo_Sistema_de_Biblioteca_POO
{
    internal class Emprestimos
    {
        #region Atributos
        
        private Livros LivroEmprestado { get; set; }
        private Usuarios UsuarioRequerinte { get; set; }
        private DateTime DataEmprestimo { get; set; }
        
        #endregion
        
        #region Builder
        
        public Emprestimos(Livros LE, Usuarios UR)
        {
            LivroEmprestado = LE;
            UsuarioRequerinte = UR;
            DataEmprestimo = DateTime.Now;
        }
        
        public Emprestimos(Livros LE, Usuarios UR, DateTime DT)
        {
            LivroEmprestado = LE;
            UsuarioRequerinte = UR;
            DataEmprestimo = DT;
        }
        
        #endregion

        #region Getters
        
        public Livros Ft_GetLivro()
        { 
            return LivroEmprestado; 
        }

        public Usuarios Ft_GetUsuario() 
        { 
            return UsuarioRequerinte; 
        }

        public DateTime Ft_GetDataEmprestimo() 
        { 
            return DataEmprestimo; 
        }
        
        #endregion

        #region ExibirInfo
        
        public void Ft_ExibirInformacoesAtraso()
        {
            Ft_GetUsuario().Ft_FullExibirInformacoes();
            Ft_GetLivro().Ft_ExibirInformacoes();
            Console.WriteLine($"Data de Empréstimo: {(Ft_GetDataEmprestimo().Day > 9 ? Ft_GetDataEmprestimo().Day : "0" + Ft_GetDataEmprestimo().Day)}/{(Ft_GetDataEmprestimo().Month > 9 ? Ft_GetDataEmprestimo().Month : "0" + Ft_GetDataEmprestimo().Month)}/{Ft_GetDataEmprestimo().Year,-23}");
            Console.WriteLine("");
        }

        public void Ft_ExibirInformacoesUsuario()
        {
            Console.WriteLine("");
            Console.WriteLine($"Data de Empréstimo: {(Ft_GetDataEmprestimo().Day > 9 ? Ft_GetDataEmprestimo().Day : "0" + Ft_GetDataEmprestimo().Day)}/{(Ft_GetDataEmprestimo().Month > 9 ? Ft_GetDataEmprestimo().Month : "0" + Ft_GetDataEmprestimo().Month)}/{Ft_GetDataEmprestimo().Year,-23}");
            Ft_GetUsuario().Ft_ExibirInformacoes();
            
        }

        public void Ft_ExibirInformacoesLivro()
        {
            Ft_GetLivro().Ft_ExibirInformacoes();
            Console.Write($"Data de Empréstimo: {(Ft_GetDataEmprestimo().Day > 9 ? Ft_GetDataEmprestimo().Day : "0" + Ft_GetDataEmprestimo().Day)}/{(Ft_GetDataEmprestimo().Month > 9 ? Ft_GetDataEmprestimo().Month : "0" + Ft_GetDataEmprestimo().Month)}/{Ft_GetDataEmprestimo().Year,-23}");
        }
        
        #endregion
    }
}
